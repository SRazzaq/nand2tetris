using System;

namespace VMTranslator
{
    internal class PushLocal : PushCommand
    {
        private const string SEGMENT = "@LCL";
        private string index;

        public PushLocal(string index)
        {
            this.index = index;
        }
        protected override string LoadSegmentToD
        {
            get
            {
                return
                    "@" + index + Environment.NewLine +
                    "D=A" + Environment.NewLine +
                    SEGMENT + Environment.NewLine +
                    "A=D+M" + Environment.NewLine +
                    "D=M" + Environment.NewLine;
            }
        }
    }
}
