using JackAnalyzer.Visitor;

namespace JackAnalyzer.AST
{
    public class KeywordConstantTerm : Term
    {
        public string Value;

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
