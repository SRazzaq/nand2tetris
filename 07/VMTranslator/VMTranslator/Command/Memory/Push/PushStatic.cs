using System;

namespace VMTranslator
{
    internal class PushStatic : PushCommand
    {
        private string fileName;
        private string index;

        public PushStatic(string fileName, string index)
        {
            this.fileName = fileName;
            this.index = index;
        }
        protected override string LoadSegmentToD
        {
            get
            {
                return
                    string.Format("@{0}.{1}", fileName, int.Parse(index)) + Environment.NewLine +
                    "D=M" + Environment.NewLine;
            }
        }
    }
}
