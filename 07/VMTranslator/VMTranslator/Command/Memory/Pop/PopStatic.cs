using System;
namespace VMTranslator
{
    internal class PopStatic : PopCommand
    {
        private int StaticBase = 16;
        private string index;

        public PopStatic(string index)
        {
            this.index = index;
        }

        public override string LoadSegmentAddressToTemp
        {
            get
            {
                return
                    "@" + (StaticBase + int.Parse(index)) + Environment.NewLine +
                    "D=A" + Environment.NewLine +
                    TEMP + Environment.NewLine +
                    "M=D" + Environment.NewLine;
            }
        }
    }
}