using JackCompiler.Visitor;

namespace JackCompiler.AST
{
    public abstract class ASTNode
    {
        public abstract void Accept(IVisitor visitor);
    }
}
