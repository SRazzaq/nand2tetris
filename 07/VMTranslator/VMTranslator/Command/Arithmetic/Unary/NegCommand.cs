using System;

namespace VMTranslator
{
    class NegateCommand : UnaryCommand
    {
        public override string Command
        {
            get
            {
                return "M=-M" + Environment.NewLine;
            }
        }
    }
}
