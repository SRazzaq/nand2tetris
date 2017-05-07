using JackCompiler.Visitor;

namespace JackCompiler.AST
{
    public class StringConstantTerm : Term
    {
        public string Value;

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
