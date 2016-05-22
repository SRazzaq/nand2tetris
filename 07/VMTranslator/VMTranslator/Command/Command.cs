using System;

namespace VMTranslator
{
    internal abstract class Command
    {
        public abstract string Code
        {
            get;
        }

        internal string IncrementStack
        {
            get
            {
                return
                    "@SP" + Environment.NewLine +
                    "AM=M+1" + Environment.NewLine;
            }
        }

        internal string DecrementStack
        {
            get
            {
                return
                    "@SP" + Environment.NewLine +
                    "AM=M-1" + Environment.NewLine;
            }
        }

        internal string LoadStackToD
        {
            get
            {
                return
                    "@SP" + Environment.NewLine +
                    "A=M" + Environment.NewLine +
                    "D=M" + Environment.NewLine;

            }
        }

        internal string LoadDToStack
        {
            get
            {
                return
                    "@SP" + Environment.NewLine +
                    "A=M" + Environment.NewLine +
                    "M=D" + Environment.NewLine;
            }
        }
    }
}