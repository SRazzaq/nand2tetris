using JackAnalyzer.Visitor;

namespace JackAnalyzer.AST
{
    public class Expression : ASTNode
    {
        public Term Term { get; set; }
        public string Operator { get; set; }
        public Term OtherTerm { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
