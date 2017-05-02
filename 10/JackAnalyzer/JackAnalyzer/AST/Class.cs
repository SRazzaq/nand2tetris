using JackAnalyzer.Visitor;
using System.Collections.Generic;

namespace JackAnalyzer.AST
{
    public class Class : ASTNode
    {
        public Class()
        {
            this.ClassVarDecs = new List<ClassVarDec>();
            this.SubrouteDecs = new List<SubrouteDec>();
        }

        public string Name { get; set; }

        public IList<ClassVarDec> ClassVarDecs { get; private set; }
        public IList<SubrouteDec> SubrouteDecs { get; private set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
