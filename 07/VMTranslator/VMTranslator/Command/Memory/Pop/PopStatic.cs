using System;
namespace VMTranslator
{
    internal class PopStatic : PopCommand
    {
        private string fileName;
        private string index;

        public PopStatic(string fileName, string index)
        {
            this.fileName = fileName;
            this.index = index;
        }

        public override string LoadSegmentAddressToTemp
        {
            get
            {
                return
                    string.Format("@{0}.{1}", fileName, index) + Environment.NewLine +
                    "D=A" + Environment.NewLine +
                    TEMP + Environment.NewLine +
                    "M=D" + Environment.NewLine;
            }
        }
    }
}