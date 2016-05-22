using System;

namespace VMTranslator
{
    internal class PushTemp : PushCommand
    {
        private int TempBase = 5;
        private string index;

        public PushTemp(string index)
        {
            this.index = index;
        }
        protected override string LoadSegmentToD
        {
            get
            {
                return
                    "@" + (TempBase + int.Parse(index)) + Environment.NewLine +
                    "D=M" + Environment.NewLine;
            }
        }
    }
}
