using JackCompiler.Visitor;

namespace JackCompiler.AST
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
