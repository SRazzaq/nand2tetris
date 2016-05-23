using System.IO;

namespace VMTranslator
{
    class CodeWriter
    {
        private StreamWriter writer;

        public CodeWriter(string asmFile)
        {
            this.writer = new StreamWriter(asmFile);
        }

        internal void Write(string code)
        {
            writer.Write(code);
        }
    }
}
