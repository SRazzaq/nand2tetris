using System;
namespace VMTranslator
{
    internal class Call : Command
    {
        private string funcationName;
        private string argumentCount;

        private static int CallCount = 0;

        public Call(string functionName, string argumentCount)
        {
            this.funcationName = functionName;
            this.argumentCount = argumentCount;
        }

        public override string Code
        {
            get
            {
                CallCount++;

                return
                    PushReturn +
                    PushLCL() +
                    PushARG() +
                    PushTHIS() +
                    PushTHAT() +
                    RepositionArgs() +
                    RepositionLCL() +
                    GotoFunction() +
                    LabelReturn();
            }
        }

        public string PushReturn
        {
            get
            {
                return
                    string.Format("@RETURN.{0}", CallCount) + Environment.NewLine +
                    "D=A" + Environment.NewLine +
                    "@SP" + Environment.NewLine +
                    "A=M" + Environment.NewLine +
                    "M=D" + Environment.NewLine +
                    IncrementStack;
            }
        }

        private string PushLCL()
        {
            // push LCL
            return PushToStack("@LCL");
        }

        private string PushARG()
        {
            // push ARG
            return PushToStack("@ARG");
        }

        private string PushTHIS()
        {
            // push THIS
            return PushToStack("@THIS");
        }

        private string PushTHAT()
        {
            // push THAT
            return PushToStack("@THAT");
        }

        private string RepositionArgs()
        {
            //ARG = SP-n-5
            return
                "@SP" + Environment.NewLine +
                "D=M" + Environment.NewLine +
                "@" + argumentCount + Environment.NewLine +
                "D=D-A" + Environment.NewLine +
                "@5" + Environment.NewLine +
                "D=D-A" + Environment.NewLine +
                "@ARG" + Environment.NewLine +
                "M=D" + Environment.NewLine;
        }

        private string RepositionLCL()
        {
            //LCL = SP
            return
                "@SP" + Environment.NewLine +
                "D=M" + Environment.NewLine +
                "@LCL" + Environment.NewLine +
                "M=D" + Environment.NewLine;
        }

        private string GotoFunction()
        {
            //goto f
            return
                "@" + funcationName + Environment.NewLine +
                "0;JMP" + Environment.NewLine;
        }

        private string LabelReturn()
        {
            // label for the return address 
            return
                string.Format("(RETURN.{0})", CallCount) + Environment.NewLine;
        }

        private string PushToStack(string segment)
        {
            return
                segment + Environment.NewLine +
                "D=M" + Environment.NewLine +
                "@SP" + Environment.NewLine +
                "A=M" + Environment.NewLine +
                "M=D" + Environment.NewLine +
                IncrementStack;
        }
    }
}