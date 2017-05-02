using JackAnalyzer.Visitor;

namespace JackAnalyzer.AST
{
    public abstract class ASTNode
    {
        public abstract void Accept(IVisitor visitor);
    }
}
