using JackAnalyzer.AST;
using System.IO;
using System.Linq;

namespace JackAnalyzer.Visitor
{
    public class PrintVisitor : IVisitor
    {
        private StreamWriter writer;

        private int indentLevel = 0;
        private int indentSize = 2;

        public PrintVisitor(StreamWriter writer)
        {
            this.writer = writer;
        }

        private void WriteLine(string s)
        {
            writer.WriteLine("{0}{1}", new string(' ', indentLevel * indentSize), s);
        }

        private void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }

        private void WriteHeader(string s)
        {
            WriteLine(s);
            indentLevel++;
        }

        private void WriteFooter(string s)
        {
            indentLevel--;
            WriteLine(s);
        }

        public void Visit(Class _class)
        {
            WriteHeader("<class>");

            WriteLine("<keyword> class </keyword>");
            WriteLine("<identifier> {0} </identifier>", _class.Name);
            WriteLine("<symbol> { </symbol>");

            foreach (var classVarDec in _class.ClassVarDecs)
            {
                classVarDec.Accept(this);
            }

            foreach (var subRouteDec in _class.SubrouteDecs)
            {
                subRouteDec.Accept(this);
            }

            WriteLine("<symbol> } </symbol>");

            WriteFooter("</class>");
        }

        public void Visit(ClassVarDec classVarDec)
        {
            WriteHeader("<classVarDec>");

            WriteLine("<keyword> {0} </keyword>", classVarDec.Modifier);
            switch (classVarDec.Type)
            {
                case "char":
                case "boolean":
                case "int":
                    WriteLine("<keyword> {0} </keyword>", classVarDec.Type);
                    break;
                default:
                    WriteLine("<identifier> {0} </identifier>", classVarDec.Type);
                    break;
            }

            foreach (var name in classVarDec.Names)
            {
                WriteLine("<identifier> {0} </identifier>", name);
                if (name != classVarDec.Names.Last())
                    WriteLine("<symbol> , </symbol>");
            }

            WriteLine("<symbol> ; </symbol>");

            WriteFooter("</classVarDec>");
        }

        public void Visit(SubrouteDec subrouteDec)
        {
            WriteHeader("<subroutineDec>");

            WriteLine("<keyword> {0} </keyword>", subrouteDec.Type);
            if (subrouteDec.ReturnType == "void")
            {
                WriteLine("<keyword> void </keyword>");
            }
            else
            {
                WriteLine("<identifier> {0} </identifier>", subrouteDec.ReturnType);
            }
            WriteLine("<identifier> {0} </identifier>", subrouteDec.Name);

            WriteLine("<symbol> ( </symbol>");

            WriteHeader("<parameterList>");
            foreach (var parameter in subrouteDec.Parameters)
            {
                parameter.Accept(this);
                if (parameter != subrouteDec.Parameters.Last())
                    WriteLine("<symbol> , </symbol>");
            }
            WriteFooter("</parameterList>");

            WriteLine("<symbol> ) </symbol>");

            subrouteDec.SubroutineBody.Accept(this);

            WriteFooter("</subroutineDec>");
        }

        public void Visit(SubroutineBody subroutineBody)
        {
            WriteHeader("<subroutineBody>");

            WriteLine("<symbol> { </symbol>");

            foreach (var varDec in subroutineBody.VarDecs)
            {
                varDec.Accept(this);
            }

            if (subroutineBody.Statements.Count > 0)
            {
                WriteHeader("<statements>");
                foreach (var statement in subroutineBody.Statements)
                {
                    statement.Accept(this);
                }
                WriteFooter("</statements>");
            }

            WriteLine("<symbol> } </symbol>");

            WriteFooter("</subroutineBody>");
        }

        public void Visit(Parameter parameter)
        {
            WriteLine("<keyword> {0} </keyword>", parameter.Type);
            WriteLine("<identifier> {0} </identifier>", parameter.Name);
        }

        public void Visit(VarDec varDec)
        {
            WriteHeader("<varDec>");

            WriteLine("<keyword> var </keyword>");
            switch (varDec.Type)
            {
                case "char":
                case "boolean":
                case "int":
                    WriteLine("<keyword> {0} </keyword>", varDec.Type);
                    break;
                default:
                    WriteLine("<identifier> {0} </identifier>", varDec.Type);
                    break;
            }
            foreach (var name in varDec.Names)
            {
                WriteLine("<identifier> {0} </identifier>", name);
                if (name != varDec.Names.Last())
                    WriteLine("<symbol> , </symbol>");
            }
            WriteLine("<symbol> ; </symbol>");

            WriteFooter("</varDec>");
        }

        public void Visit(DoStatement doStatement)
        {
            WriteHeader("<doStatement>");

            WriteLine("<keyword> do </keyword>");
            if (doStatement.ClassName != null)
            {
                WriteLine("<identifier> {0} </identifier>", doStatement.ClassName);
                WriteLine("<symbol> . </symbol>");
            }

            WriteLine("<identifier> {0} </identifier>", doStatement.SubroutineName);
            WriteLine("<symbol> ( </symbol>");

            WriteHeader("<expressionList>");
            foreach (var expression in doStatement.Expressions)
            {
                expression.Accept(this);
                if (expression != doStatement.Expressions.Last())
                    WriteLine("<symbol> , </symbol>");
            }
            WriteFooter("</expressionList>");

            WriteLine("<symbol> ) </symbol>");
            WriteLine("<symbol> ; </symbol>");
            WriteFooter("</doStatement>");
        }

        public void Visit(IfStatement ifStatement)
        {
            WriteHeader("<ifStatement>");
            WriteLine("<keyword> if </keyword>");

            WriteLine("<symbol> ( </symbol>");
            ifStatement.Condition.Accept(this);
            WriteLine("<symbol> ) </symbol>");

            WriteLine("<symbol> { </symbol>");
            if (ifStatement.IfStatements.Count > 0)
            {
                WriteHeader("<statements>");
                foreach (var statement in ifStatement.IfStatements)
                {
                    statement.Accept(this);
                }
                WriteFooter("</statements>");
            }
            WriteLine("<symbol> } </symbol>");

            if (ifStatement.ElseStatements.Count > 0)
            {
                WriteLine("<keyword> else </keyword>");
                WriteLine("<symbol> { </symbol>");
                WriteHeader("<statements>");
                foreach (var statement in ifStatement.ElseStatements)
                {
                    statement.Accept(this);
                }
                WriteFooter("</statements>");
                WriteLine("<symbol> } </symbol>");
            }

            WriteFooter("</ifStatement>");
        }

        public void Visit(LetStatement letStatement)
        {
            WriteHeader("<letStatement>");

            WriteLine("<keyword> let </keyword>");
            WriteLine("<identifier> {0} </identifier>", letStatement.Variable);
            if (letStatement.ArrayExpression != null)
            {
                WriteLine("<symbol> [ </symbol>");
                letStatement.ArrayExpression.Accept(this);
                WriteLine("<symbol> ] </symbol>");
            }

            WriteLine("<symbol> = </symbol>");

            letStatement.Expression.Accept(this);

            WriteLine("<symbol> ; </symbol>");

            WriteFooter("</letStatement>");
        }

        public void Visit(ReturnStatement returnStatement)
        {
            WriteHeader("<returnStatement>");

            WriteLine("<keyword> return </keyword>");
            if (returnStatement.Expression != null)
            {
                returnStatement.Expression.Accept(this);
            }

            WriteLine("<symbol> ; </symbol>");

            WriteFooter("</returnStatement>");
        }

        public void Visit(WhileStatement whileStatement)
        {
            WriteHeader("<whileStatement>");

            WriteLine("<keyword> while </keyword>");

            WriteLine("<symbol> ( </symbol>");
            whileStatement.Condition.Accept(this);
            WriteLine("<symbol> ) </symbol>");

            WriteLine("<symbol> { </symbol>");
            WriteHeader("<statements>");
            foreach (var statement in whileStatement.Statements)
            {
                statement.Accept(this);
            }
            WriteFooter("</statements>");
            WriteLine("<symbol> } </symbol>");

            WriteFooter("</whileStatement>");
        }

        public void Visit(Expression expression)
        {
            WriteHeader("<expression>");

            expression.Term.Accept(this);
            if (expression.Operator != null)
            {
                var op = expression.Operator
                                .Replace("&", "&amp;")
                                .Replace("<", "&lt;")
                                .Replace(">", "&gt;");
                WriteLine("<symbol> {0} </symbol>", op);
                expression.OtherTerm.Accept(this);
            }

            WriteFooter("</expression>");
        }


        public void Visit(VariableTerm variableTerm)
        {
            WriteHeader("<term>");

            WriteLine("<identifier> {0} </identifier>", variableTerm.Variable);
            if (variableTerm.ArrayExpression != null)
            {
                WriteLine("<symbol> [ </symbol>");
                variableTerm.ArrayExpression.Accept(this);
                WriteLine("<symbol> ] </symbol>");
            }

            WriteFooter("</term>");
        }

        public void Visit(ExpressionTerm expressionTerm)
        {
            WriteHeader("<term>");

            WriteLine("<symbol> ( </symbol>");
            expressionTerm.Expression.Accept(this);
            WriteLine("<symbol> ) </symbol>");


            WriteFooter("</term>");
        }

        public void Visit(UnaryTerm unaryTerm)
        {
            WriteHeader("<term>");

            WriteLine("<symbol> {0} </symbol>", unaryTerm.Operator);
            unaryTerm.Term.Accept(this);

            WriteFooter("</term>");
        }

        public void Visit(SubroutineCallTerm subroutineCallTerm)
        {
            WriteHeader("<term>");

            if (subroutineCallTerm.ClassName != null)
            {
                WriteLine("<identifier> {0} </identifier>", subroutineCallTerm.ClassName);
                WriteLine("<symbol> . </symbol>");
            }

            WriteLine("<identifier> {0} </identifier>", subroutineCallTerm.SubroutineName);
            WriteLine("<symbol> ( </symbol>");

            WriteHeader("<expressionList>");
            foreach (var expression in subroutineCallTerm.Expressions)
            {
                expression.Accept(this);
                if (expression != subroutineCallTerm.Expressions.Last())
                    WriteLine("<symbol> , </symbol>");
            }
            WriteFooter("</expressionList>");

            WriteLine("<symbol> ) </symbol>");

            WriteFooter("</term>");
        }


        public void Visit(IntegerConstantTerm integerConstantTerm)
        {
            WriteHeader("<term>");
            WriteLine("<integerConstant> {0} </integerConstant>", integerConstantTerm.Value);
            WriteFooter("</term>");
        }


        public void Visit(StringConstantTerm stringConstantTerm)
        {
            WriteHeader("<term>");
            WriteLine("<stringConstant> {0} </stringConstant>", stringConstantTerm.Value);
            WriteFooter("</term>");
        }

        public void Visit(KeywordConstantTerm keywordConstantTerm)
        {
            WriteHeader("<term>");
            WriteLine("<keyword> {0} </keyword>", keywordConstantTerm.Value);
            WriteFooter("</term>");
        }
    }
}
