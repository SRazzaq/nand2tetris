using JackAnalyzer.Visitor;

namespace JackAnalyzer.AST
{
    public class UnaryTerm : Term
    {
        public string Operator { get; set; }
        public Term Term { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
