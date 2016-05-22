using System;

namespace VMTranslator
{
    abstract class ComparisonCommand : BinaryCommand
    {
        private static int ComparisonCount = 0;

        public override string Command
        {
            get
            {
                ComparisonCount++;

                return
                    "D=D-M" + Environment.NewLine +

                    String.Format("@TRUE.{0}", ComparisonCount) + Environment.NewLine +
                    Comparison +

                    "@SP" + Environment.NewLine +
                    "A=M" + Environment.NewLine +
                    "M=0" + Environment.NewLine +
                    String.Format("@DONE.{0}", ComparisonCount) + Environment.NewLine +
                    "0;JMP" + Environment.NewLine +

                    String.Format("(TRUE.{0})", ComparisonCount) + Environment.NewLine +
                    "@SP" + Environment.NewLine +
                    "A=M" + Environment.NewLine +
                    "M=-1" + Environment.NewLine +

                    String.Format("(DONE.{0})", ComparisonCount) + Environment.NewLine;
            }
        }

        public abstract string Comparison
        {
            get;
        }
    }
}
