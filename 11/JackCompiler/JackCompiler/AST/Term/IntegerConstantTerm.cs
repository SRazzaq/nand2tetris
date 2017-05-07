using JackCompiler.Visitor;

namespace JackCompiler.AST
{
    public class IntegerConstantTerm : Term
    {
        public int Value;

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
