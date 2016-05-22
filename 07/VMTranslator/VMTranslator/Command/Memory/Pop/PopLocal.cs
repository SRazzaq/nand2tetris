using System;
namespace VMTranslator
{
    internal class PopLocal : PopCommand
    {
        private const string SEGMENT = "@LCL";
        private string index;

        public PopLocal(string index)
        {
            this.index = index;
        }

        public override string LoadSegmentAddressToTemp
        {
            get
            {
                return
                    "@" + index + Environment.NewLine +
                    "D=A" + Environment.NewLine +
                    SEGMENT + Environment.NewLine +
                    "D=D+M" + Environment.NewLine +
                    TEMP + Environment.NewLine +
                    "M=D" + Environment.NewLine;
            }
        }
    }
}