using System;

namespace VMTranslator
{
    internal class PushConstant : PushCommand
    {
        private string index;

        public PushConstant(string index)
        {
            this.index = index;
        }
        protected override string LoadSegmentToD
        {
            get
            {
                return
                    "@" + index + Environment.NewLine +
                    "D=A" + Environment.NewLine;
            }
        }
    }
}
