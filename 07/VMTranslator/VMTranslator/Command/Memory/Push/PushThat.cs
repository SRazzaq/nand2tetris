using System;

namespace VMTranslator
{
    internal class PushThat : PushCommand
    {
        private const string SEGMENT = "@THAT";
        private string index;

        public PushThat(string index)
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
