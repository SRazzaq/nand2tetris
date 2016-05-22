using System;
namespace VMTranslator
{
    internal class PopTemp : PopCommand
    {
        private int TempBase = 5;
        private string index;

        public PopTemp(string index)
        {
            this.index = index;
        }

        public override string LoadSegmentAddressToTemp
        {
            get
            {
                return
                    "@" + (TempBase + int.Parse(index)) + Environment.NewLine +
                    "D=A" + Environment.NewLine +
                    TEMP + Environment.NewLine +
                    "M=D" + Environment.NewLine;
            }
        }
    }
}