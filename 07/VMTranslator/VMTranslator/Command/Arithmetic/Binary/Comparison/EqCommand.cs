using System;

namespace VMTranslator
{
    class EqCommand : ComparisonCommand
    {
        public override string Comparison
        {
            get
            {
                return "D;JEQ" + Environment.NewLine;
            }
        }
    }
}
