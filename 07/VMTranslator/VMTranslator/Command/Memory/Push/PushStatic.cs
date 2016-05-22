using System;

namespace VMTranslator
{
    internal class PushStatic : PushCommand
    {
        private int StaticBase = 16;
        private string index;

        public PushStatic(string index)
        {
            this.index = index;
        }
        protected override string LoadSegmentToD
        {
            get
            {
                return
                    "@" + (StaticBase + int.Parse(index)) + Environment.NewLine +
                    "D=M" + Environment.NewLine;
            }
        }
    }
}
