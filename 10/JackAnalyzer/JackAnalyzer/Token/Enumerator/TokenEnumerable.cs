using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer
{
    public class Scanner : IEnumerable<Token>
    {
        private StreamReader reader;

        public Scanner(StreamReader reader)
        {
            this.reader = reader;
        }

        public IEnumerator<Token> GetEnumerator()
        {
            return new TokenEnumerator(reader);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}