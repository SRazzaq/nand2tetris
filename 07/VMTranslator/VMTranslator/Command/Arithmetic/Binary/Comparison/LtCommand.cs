using System;

namespace VMTranslator
{
    class LtCommand : ComparisonCommand
    {
        public override string Comparison
        {
            get
            {
                return "D;JGT" + Environment.NewLine;
            }
        }
    }
}
