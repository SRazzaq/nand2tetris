using JackCompiler.Visitor;

namespace JackCompiler.AST
{
    public class ExpressionTerm : Term
    {
        public Expression Expression { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
