using JackAnalyzer.Visitor;

namespace JackAnalyzer.AST
{
    public class Parameter : ASTNode
    {
        public string Type { get; set; }
        public string Name { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
