using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Assembler
{
    public class Parser : IEnumerable<Command>
    {
        private StreamReader file;

        public Parser(StreamReader file)
        {
            this.file = file;
        }

        public IEnumerator<Command> GetEnumerator()
        {
            return new ParserEnumerator(file);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}