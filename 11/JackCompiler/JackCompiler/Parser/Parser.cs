using System;
using System.Collections.Generic;

namespace JackCompiler.Parser
{
    public abstract class Parser
    {
        protected IEnumerator<Token> scanner;

        public Parser(IEnumerator<Token> scanner)
        {
            this.scanner = scanner;
        }

        protected Token CurrentToken
        {
            get
            {
                if (scanner.Current == null) scanner.MoveNext();
                return scanner.Current;
            }
        }

        // Validates token value matches and writes to the output file.
        protected Token GetToken<T>(string value)
        {
            if (CurrentToken.Value != value)
                throw new Exception(string.Format("Expected token '{0}' not found.", value));

            return GetToken<T>();
        }

        // Validates is of correct type and writes to the output file.
        protected Token GetToken<T>()
        {
            if (!(CurrentToken is T))
                throw new Exception(string.Format("Invalid token found."));

            var token = CurrentToken;
            scanner.MoveNext();

            return token;
        }
    }
}
