using System;

namespace VMTranslator
{
    internal class IfGoto : Command
    {
        private string name;

        public IfGoto(string name)
        {
            this.name = name;
        }

        public override string Code
        {
            get
            {
                return
                    DecrementStack +
                    LoadStackToD + Environment.NewLine +
                    "@" + name + Environment.NewLine +
                    "D;JNE" + Environment.NewLine;
            }
        }
    }
}