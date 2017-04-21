namespace JackAnalyzer
{
    internal class SymbolToken : Token
    {
        public SymbolToken(string value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return string.Format("<symbol> {0} </symbol>", System.Security.SecurityElement.Escape(Value));
        }
    }
}