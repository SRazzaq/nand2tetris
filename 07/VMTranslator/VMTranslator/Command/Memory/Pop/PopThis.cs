using System;
namespace VMTranslator
{
    internal class PopThis : PopCommand
    {
        private const string SEGMENT = "@THIS";
        private string index;

        public PopThis(string index)
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