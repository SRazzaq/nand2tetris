using System;

namespace VMTranslator
{
    class OrCommand : BinaryCommand
    {
        public override string Command
        {
            get
            {
                return "M=D|M" + Environment.NewLine;
            }
        }
    }
}
