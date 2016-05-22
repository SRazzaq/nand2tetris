using System;

namespace VMTranslator
{
    internal class PushArgument : PushCommand
    {
        private const string SEGMENT = "@ARG";
        private string index;

        public PushArgument(string index)
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
