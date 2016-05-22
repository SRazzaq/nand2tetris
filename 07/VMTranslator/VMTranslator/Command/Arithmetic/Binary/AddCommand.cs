using System;

namespace VMTranslator
{
    class AddCommand : BinaryCommand
    {
        public override string Command
        {
            get
            {
                return "M=M+D" + Environment.NewLine;
            }
        }
    }
}
