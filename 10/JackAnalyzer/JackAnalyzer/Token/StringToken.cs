namespace JackAnalyzer
{
    internal class StringToken : Token
    {
        public StringToken(string value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return string.Format("<stringConstant> {0} </stringConstant>", Value);
        }
    }
}