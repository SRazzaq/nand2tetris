using JackAnalyzer.Visitor;
using System.Collections.Generic;

namespace JackAnalyzer.AST
{
    public class ClassVarDec : ASTNode
    {
        public ClassVarDec()
        {
            this.Names = new List<string>();
        }

        public string Modifier { get; set; } // static | field
        public string Type { get; set; }
        public IList<string> Names { get; private set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
