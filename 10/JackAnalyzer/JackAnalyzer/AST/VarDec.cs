using System.Collections.Generic;
namespace JackAnalyzer.AST
{
    public class VarDec : ASTNode
    {
        public VarDec()
        {
            this.Names = new List<string>();
        }

        public string Type { get; set; }
        public IList<string> Names { get; private set; }

        public override void Accept(Visitor.IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
