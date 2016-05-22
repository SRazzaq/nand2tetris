using System;

namespace VMTranslator
{
    internal class PushPointer : PushCommand
    {
        private const string THIS = "@THIS";
        private const string THAT = "@THAT";
        private string index;

        public PushPointer(string index)
        {
            this.index = index;
        }
        protected override string LoadSegmentToD
        {
            get
            {
                var pointer = index == "0" ? THIS : THAT;
                return
                    pointer + Environment.NewLine +
                    "D=M" + Environment.NewLine;
            }
        }
    }
}
