using System;

namespace VMTranslator
{
    internal class PushThis : PushCommand
    {
        private const string SEGMENT = "@THIS";
        private string index;

        public PushThis(string index)
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
