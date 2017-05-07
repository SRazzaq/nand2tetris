using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace JackCompiler
{
    public class Tokenizer : IEnumerable<Token>
    {
        private StreamReader reader;

        public Tokenizer(StreamReader reader)
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