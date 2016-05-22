using System;

namespace VMTranslator
{
    class GtCommand : ComparisonCommand
    {
        public override string Comparison
        {
            get
            {
                return "D;JLT" + Environment.NewLine;
            }
        }
    }
}
