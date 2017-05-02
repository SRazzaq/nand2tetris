using JackAnalyzer.Visitor;

namespace JackAnalyzer.AST
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
