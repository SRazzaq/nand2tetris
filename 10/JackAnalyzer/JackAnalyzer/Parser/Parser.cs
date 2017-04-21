using System;
using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer.Parser
{
    public abstract class Parser
    {
        protected IEnumerator<Token> scanner;
        protected StreamWriter writer;

        public Parser(IEnumerator<Token> scanner, StreamWriter writer)
        {
            this.scanner = scanner;
            this.writer = writer;
        }

        public abstract void Parse();

        protected Token CurrentToken
        {
            get
            {
                if (scanner.Current == null) scanner.MoveNext();
                return scanner.Current;
            }
        }

        // Validates token value matches and writes to the output file.
        protected void WriteToken<T>(string value)
        {
            if (CurrentToken.Value != value)
                throw new Exception(string.Format("Expected token '{0}' not found.", value));

            WriteToken<T>();
        }

        // Validates is of correct type and writes to the output file.
        protected void WriteToken<T>()
        {
            if (!(CurrentToken is T))
                throw new Exception(string.Format("Invalid token found."));

            writer.WriteLine(CurrentToken);

            scanner.MoveNext();
        }

        protected void WriteLine(string t)
        {
            writer.WriteLine(t);
        }
    }
}
