using JackCompiler.Visitor;
using System.Collections.Generic;

namespace JackCompiler.AST
{
    public class SubroutineBody : ASTNode
    {
        public SubroutineBody()
        {
            this.VarDecs = new List<VarDec>();
            this.Statements = new List<Statement>();
        }

        public IList<VarDec> VarDecs { get; private set; }
        public IList<Statement> Statements { get; private set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
