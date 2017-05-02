namespace JackAnalyzer
{
    internal class KeywordToken : Token
    {
        public KeywordToken(string value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return string.Format("<keyword> {0} </keyword>", Value);
        }
    }
}