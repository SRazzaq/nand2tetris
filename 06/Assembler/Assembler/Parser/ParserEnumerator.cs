using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Assembler
{
    public class ParserEnumerator : IEnumerator<Command>
    {
        private StreamReader file;
        private Command current;

        public ParserEnumerator(StreamReader file)
        {
            this.file = file;
        }

        public Command Current
        {
            get
            {
                return current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public bool MoveNext()
        {
            do
            {
                var line = file.ReadLine();
                if (line != null)
                {
                    var command = RemoveComments(line);
                    if (command.StartsWith("@"))
                    {
                        current = new ACommand(command);
                        return true;
                    }
                    else if (command.StartsWith("(") && command.EndsWith(")"))
                    {
                        current = new LCommand(command);
                        return true;
                    }
                    else if (!string.IsNullOrEmpty(command))
                    {
                        current = new CCommand(command);
                        return true;
                    }
                }
            } while (!file.EndOfStream);

            return false;
        }

        public void Reset()
        {
            file.BaseStream.Position = 0;
            file.DiscardBufferedData();
        }

        public void Dispose()
        {
            // Do nothing
        }

        private string RemoveComments(string line)
        {
            var lineParts = line.Split(new string[] { "//" }, System.StringSplitOptions.None);
            return lineParts[0].Trim();
        }

    }
}
