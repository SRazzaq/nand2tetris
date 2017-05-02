using JackAnalyzer.Visitor;

namespace JackAnalyzer.AST
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
