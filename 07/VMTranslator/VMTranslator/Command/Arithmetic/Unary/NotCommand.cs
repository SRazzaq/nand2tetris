using System;

namespace VMTranslator
{
    class NotCommand : UnaryCommand
    {
        public override string Command
        {
            get
            {
                return "M=!M" + Environment.NewLine;
            }
        }
    }
}
