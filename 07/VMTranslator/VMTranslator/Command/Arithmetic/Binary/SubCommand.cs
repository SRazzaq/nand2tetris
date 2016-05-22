using System;

namespace VMTranslator
{
    class SubCommand : BinaryCommand
    {
        public override string Command
        {
            get
            {
                return "M=M-D" + Environment.NewLine;
            }
        }
    }
}
