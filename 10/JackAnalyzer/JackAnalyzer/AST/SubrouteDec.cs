using JackAnalyzer.Visitor;
using System.Collections.Generic;

namespace JackAnalyzer.AST
{
    public class SubrouteDec : ASTNode
    {
        public SubrouteDec()
        {
            this.Parameters = new List<Parameter>();
        }

        public string Type { get; set; }
        public string ReturnType { get; set; }
        public string Name { get; set; }

        public IList<Parameter> Parameters { get; private set; }

        public SubroutineBody SubroutineBody { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
