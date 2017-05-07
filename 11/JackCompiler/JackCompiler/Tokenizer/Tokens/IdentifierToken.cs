namespace JackCompiler
{
    internal class IdentifierToken : Token
    {
        public IdentifierToken(string value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return string.Format("<identifier> {0} </identifier>", Value);
        }
    }
}