using System;

namespace VMTranslator
{
    internal class Goto : Command
    {
        private string name;

        public Goto(string name)
        {
            this.name = name;
        }

        public override string Code
        {
            get
            {
                return
                    "@" + name + Environment.NewLine +
                    "0;JMP" + Environment.NewLine;
            }
        }
    }
}