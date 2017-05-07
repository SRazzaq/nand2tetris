using JackCompiler.AST;

namespace JackCompiler.Visitor
{
    public interface IVisitor
    {
        void Visit(Class _class);
        void Visit(ClassVarDec classVarDec);
        void Visit(SubrouteDec subrouteDec);

        void Visit(SubroutineBody subroutineBody);
        void Visit(Parameter parameter);
        void Visit(VarDec varDec);

        void Visit(DoStatement doStatement);
        void Visit(IfStatement ifStatement);
        void Visit(LetStatement letStatement);
        void Visit(ReturnStatement returnStatement);
        void Visit(WhileStatement whileStatement);

        void Visit(Expression expression);

        void Visit(IntegerConstantTerm integerConstantTerm);
        void Visit(StringConstantTerm stringConstantTerm);
        void Visit(KeywordConstantTerm keywordConstantTerm);
        void Visit(VariableTerm variableTerm);
        void Visit(UnaryTerm unaryTerm);
        void Visit(ExpressionTerm expressionTerm);
        void Visit(SubroutineCallTerm subroutineCallTerm);
    }
}
