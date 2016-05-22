using System;

namespace VMTranslator
{
    class AndCommand : BinaryCommand
    {
        public override string Command
        {
            get
            {
                return "M=D&M" + Environment.NewLine;
            }
        }
    }
}
