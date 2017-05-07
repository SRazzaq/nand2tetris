using JackCompiler.Visitor;

namespace JackCompiler.AST
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
