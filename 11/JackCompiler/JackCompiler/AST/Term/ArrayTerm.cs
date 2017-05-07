using JackCompiler.Visitor;

namespace JackCompiler.AST
{
    public class VariableTerm : Term
    {
        public string Variable { get; set; }
        public Expression ArrayExpression { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
