namespace JackAnalyzer
{
    internal class IntegerToken : Token
    {
        public IntegerToken(string value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return string.Format("<integerConstant> {0} </integerConstant>", Value);
        }
    }
}