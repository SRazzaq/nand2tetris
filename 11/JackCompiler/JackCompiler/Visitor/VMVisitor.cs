using JackCompiler.AST;
using System.Collections.Generic;
using System.IO;

namespace JackCompiler.Visitor
{
    public class VMVisitor : IVisitor
    {
        private StreamWriter writer;
        private SymbolsTable symbolsTable;

        private string currentClass;

        private int whileCount = 0;
        private int ifCount = 0;

        public VMVisitor(StreamWriter writer)
        {
            this.writer = writer;
            this.symbolsTable = new SymbolsTable();
        }

        private void WriteLine(string s)
        {
            writer.WriteLine(s);
        }

        private void WriteLine(string format, params object[] args)
        {
            writer.WriteLine(format, args);
        }

        public void Visit(Class _class)
        {
            currentClass = _class.Name;

            foreach (var classVarDec in _class.ClassVarDecs)
            {
                classVarDec.Accept(this);
            }

            foreach (var subRouteDec in _class.SubrouteDecs)
            {
                subRouteDec.Accept(this);
            }
        }

        public void Visit(ClassVarDec classVarDec)
        {
            Kind kind = (classVarDec.Modifier == "static" ? Kind.STATIC : Kind.FIELD);
            foreach (var name in classVarDec.Names)
            {
                symbolsTable.Define(name, classVarDec.Type, kind);
            }
        }

        public void Visit(SubrouteDec subrouteDec)
        {
            symbolsTable.StartSubroutine();
            whileCount = 0;
            ifCount = 0;

            var localCount = 0;
            foreach (var varDec in subrouteDec.SubroutineBody.VarDecs)
            {
                localCount += varDec.Names.Count;
            }

            WriteLine("function {0}.{1} {2}", currentClass, subrouteDec.Name, localCount);
            switch (subrouteDec.Type)
            {
                case "constructor":
                    // Allocate memory for constructed object
                    WriteLine("push constant {0}", symbolsTable.VarCount(Kind.FIELD));
                    WriteLine("call Memory.alloc 1");

                    // point "this" pointer to current object.
                    WriteLine("pop pointer 0");

                    break;

                case "method":
                    // Define "this" as first symbol.
                    symbolsTable.Define("this", currentClass, Kind.ARG);

                    // point "this" pointer to current object.
                    WriteLine("push argument 0");
                    WriteLine("pop pointer 0");

                    break;
            }

            foreach (var parameter in subrouteDec.Parameters)
            {
                parameter.Accept(this);
            }

            subrouteDec.SubroutineBody.Accept(this);
        }

        public void Visit(SubroutineBody subroutineBody)
        {
            foreach (var varDec in subroutineBody.VarDecs)
            {
                varDec.Accept(this);
            }

            foreach (var statement in subroutineBody.Statements)
            {
                statement.Accept(this);
            }
        }

        public void Visit(Parameter parameter)
        {
            symbolsTable.Define(parameter.Name, parameter.Type, Kind.ARG);
        }

        public void Visit(VarDec varDec)
        {
            foreach (var name in varDec.Names)
            {
                symbolsTable.Define(name, varDec.Type, Kind.VAR);
            }
        }

        public void Visit(DoStatement doStatement)
        {
            CallSubroutine(doStatement.ClassName, doStatement.SubroutineName, doStatement.Expressions);
            WriteLine("pop temp 0");
        }

        public void Visit(IfStatement ifStatement)
        {
            var lc = ifCount++;

            ifStatement.Condition.Accept(this);
            WriteLine("if-goto IF_TRUE{0}", lc);
            WriteLine("goto IF_FALSE{0}", lc);

            WriteLine("label IF_TRUE{0}", lc);
            foreach (var statement in ifStatement.IfStatements)
            {
                statement.Accept(this);
            }

            if (ifStatement.ElseStatements.Count > 0)
            {
                WriteLine("goto IF_END{0}", lc);

                WriteLine("label IF_FALSE{0}", lc);
                foreach (var statement in ifStatement.ElseStatements)
                {
                    statement.Accept(this);
                }
                WriteLine("label IF_END{0}", lc);
            }
            else
            {
                WriteLine("label IF_FALSE{0}", lc);
            }
        }

        public void Visit(LetStatement letStatement)
        {
            if (letStatement.ArrayExpression != null)
            {
                // Evaluate array expression.
                letStatement.ArrayExpression.Accept(this);

                // Add it to the base Address.
                WriteLine("push {0} {1}", getSegment(symbolsTable.KindOf(letStatement.Variable)), symbolsTable.IndexOf(letStatement.Variable));
                WriteLine("add");
            }

            letStatement.Expression.Accept(this);
            if (letStatement.ArrayExpression != null)
            {
                // pop stack to *ADDR.
                WriteLine("pop temp 0");
                WriteLine("pop pointer 1");
                WriteLine("push temp 0");
                WriteLine("pop that 0");
            }
            else
            {
                WriteLine("pop {0} {1}", getSegment(symbolsTable.KindOf(letStatement.Variable)), symbolsTable.IndexOf(letStatement.Variable));
            }
        }

        public void Visit(ReturnStatement returnStatement)
        {
            if (returnStatement.Expression != null)
            {
                returnStatement.Expression.Accept(this);
            }
            else
            {
                // Nothing to return ... default to 0
                WriteLine("push constant 0");
            }
            WriteLine("return");
        }

        public void Visit(WhileStatement whileStatement)
        {
            var lc = whileCount++;

            WriteLine("label WHILE_EXP{0}", lc);

            whileStatement.Condition.Accept(this);
            WriteLine("not");
            WriteLine("if-goto WHILE_END{0}", lc);

            foreach (var statement in whileStatement.Statements)
            {
                statement.Accept(this);
            }

            WriteLine("goto WHILE_EXP{0}", lc);
            WriteLine("label WHILE_END{0}", lc);
        }

        public void Visit(Expression expression)
        {
            expression.Term.Accept(this);
            if (expression.Operator != null)
            {
                expression.OtherTerm.Accept(this);
                switch (expression.Operator)
                {
                    case "+":
                        WriteLine("add");
                        break;
                    case "-":
                        WriteLine("sub");
                        break;
                    case "*":
                        WriteLine("call Math.multiply 2");
                        break;
                    case "/":
                        WriteLine("call Math.divide 2");
                        break;

                    case "=":
                        WriteLine("eq");
                        break;
                    case "<":
                        WriteLine("lt");
                        break;
                    case ">":
                        WriteLine("gt");
                        break;

                    case "&":
                        WriteLine("and");
                        break;
                    case "|":
                        WriteLine("or");
                        break;
                }
            }
        }

        public void Visit(IntegerConstantTerm integerConstantTerm)
        {
            WriteLine("push constant {0}", integerConstantTerm.Value);
        }

        public void Visit(StringConstantTerm stringConstantTerm)
        {
            WriteLine("push constant {0}", stringConstantTerm.Value.Length);
            WriteLine("call String.new 1");

            foreach (var ch in stringConstantTerm.Value)
            {
                WriteLine("push constant {0}", (int)ch);
                WriteLine("call String.appendChar 2");
            }
        }

        public void Visit(KeywordConstantTerm keywordConstantTerm)
        {
            if (keywordConstantTerm.Value == "this")
            {
                WriteLine("push pointer 0");
                return;
            }

            // null and false
            WriteLine("push constant 0");
            if (keywordConstantTerm.Value == "true")
                WriteLine("not");
        }

        public void Visit(VariableTerm variableTerm)
        {
            if (variableTerm.ArrayExpression != null)
            {
                // Evaluate array expression.
                variableTerm.ArrayExpression.Accept(this);

                // Add it to base address.
                WriteLine("push {0} {1}", getSegment(symbolsTable.KindOf(variableTerm.Variable)), symbolsTable.IndexOf(variableTerm.Variable));
                WriteLine("add");

                // push *ADDR to stack.
                WriteLine("pop pointer 1");
                WriteLine("push that 0");
            }
            else
            {
                WriteLine("push {0} {1}", getSegment(symbolsTable.KindOf(variableTerm.Variable)), symbolsTable.IndexOf(variableTerm.Variable));
            }
        }

        public void Visit(UnaryTerm unaryTerm)
        {
            unaryTerm.Term.Accept(this);
            switch (unaryTerm.Operator)
            {
                case "-":
                    WriteLine("neg");
                    break;
                case "~":
                    WriteLine("not");
                    break;
            }
        }

        public void Visit(ExpressionTerm expressionTerm)
        {
            expressionTerm.Expression.Accept(this);
        }

        public void Visit(SubroutineCallTerm subroutineCallTerm)
        {
            CallSubroutine(subroutineCallTerm.ClassName, subroutineCallTerm.SubroutineName, subroutineCallTerm.Expressions);
        }

        private void CallSubroutine(string className, string subroutineName, IList<Expression> expressions)
        {
            if (className == null)
            {
                WriteLine("push pointer 0");
            }
            else if (symbolsTable.TypeOf(className) != null)
            {
                WriteLine("push {0} {1}", getSegment(symbolsTable.KindOf(className)), symbolsTable.IndexOf(className));
            }

            foreach (var expression in expressions)
            {
                expression.Accept(this);
            }

            if (className == null)
            {
                // no class name... local method call. Use current class name.
                WriteLine("call {0}.{1} {2}", currentClass, subroutineName, expressions.Count + 1);
            }
            else
            {
                if (symbolsTable.TypeOf(className) == null)
                {
                    // No symbol in table... system call, use className given.
                    WriteLine("call {0}.{1} {2}", className, subroutineName, expressions.Count);
                }
                else
                {
                    // symbol defined... remote call, use reference to class name.
                    WriteLine("call {0}.{1} {2}", symbolsTable.TypeOf(className), subroutineName, expressions.Count + 1);
                }
            }
        }

        private string getSegment(Kind kind)
        {
            switch (kind)
            {
                case Kind.VAR:
                    return "local";
                case Kind.FIELD:
                    return "this";
                case Kind.STATIC:
                    return "static";
                case Kind.ARG:
                    return "argument";
                default:
                    return null;
            }
        }

    }
}
