using System;
namespace VMTranslator
{
    internal class PopPointer : PopCommand
    {
        private const string THIS = "@THIS";
        private const string THAT = "@THAT";
        private string index;

        public PopPointer(string index)
        {
            this.index = index;
        }

        public override string LoadSegmentAddressToTemp
        {
            get
            {
                var pointer = index == "0" ? THIS : THAT;
                return
                    pointer + Environment.NewLine +
                    "D=A" + Environment.NewLine +
                    TEMP + Environment.NewLine +
                    "M=D" + Environment.NewLine;
            }
        }
    }
}