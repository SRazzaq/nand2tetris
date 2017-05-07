using JackCompiler.Visitor;

namespace JackCompiler.AST
{
    public class ReturnStatement : Statement
    {
        public Expression Expression { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
