using System;

namespace VMTranslator
{
    internal abstract class PopCommand : Command
    {
        protected const string TEMP = "@R13";

        public override string Code
        {
            get
            {
                return
                    DecrementStack +
                    LoadSegmentAddressToTemp +
                    LoadStackToD +
                    LoadTempToA +
                    SetSegmentToD;
            }
        }

        public abstract string LoadSegmentAddressToTemp
        {
            get;
        }

        private string LoadTempToA
        {
            get
            {
                return
                    TEMP + Environment.NewLine +
                    "A=M" + Environment.NewLine;
            }
        }

        public string SetSegmentToD
        {
            get
            {
                return "M=D" + Environment.NewLine;
            }
        }
    }
}