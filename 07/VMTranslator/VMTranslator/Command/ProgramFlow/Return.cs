using System;
namespace VMTranslator
{
    internal class Return : Command
    {
        protected const string FRAME = "@R13";
        protected const string RETURN = "@R14";
        public override string Code
        {
            get
            {
                return
                    SetFrame() +
                    SetReturnAddress() +

                    SetReturnValue() +
                    SetSP() +
                    SetTHAT() +
                    SetTHIS() +
                    SetARG() +
                    SetLCL() +

                    GotoReturn();

            }
        }

        private string SetFrame()
        {
            // FRAME = LCL
            return
                "@LCL" + Environment.NewLine +
                "D=M" + Environment.NewLine +
                FRAME + Environment.NewLine +
                "M=D" + Environment.NewLine;
        }

        private string SetReturnAddress()
        {
            // RET=*(FRAME-5)
            return SegmentToFrameMinus(RETURN, 5);
        }

        private string SetReturnValue()
        {
            // *ARG=pop()
            return
                DecrementStack +
                "D=M" + Environment.NewLine +
                "@ARG" + Environment.NewLine +
                "A=M" + Environment.NewLine +
                "M=D" + Environment.NewLine;
        }

        private string SetSP()
        {
            // SP=ARG+1
            return
                "@ARG" + Environment.NewLine +
                "D=M+1" + Environment.NewLine +
                "@SP" + Environment.NewLine +
                "M=D" + Environment.NewLine;
        }

        private string SetTHAT()
        {
            // THAT=*(FRAME-1)
            return SegmentToFrameMinus("@THAT", 1);
        }

        private string SetTHIS()
        {
            // THIS=*(FRAME-2)
            return SegmentToFrameMinus("@THIS", 2);
        }

        private string SetARG()
        {
            // ARG=*(FRAME-3) 
            return SegmentToFrameMinus("@ARG", 3);
        }

        private string SetLCL()
        {
            // LCL=*(FRAME-4)
            return SegmentToFrameMinus("@LCL", 4);
        }

        private string GotoReturn()
        {
            // goto RET 
            return 
                RETURN + Environment.NewLine +
                "A=M" + Environment.NewLine +
                "0;JMP" + Environment.NewLine;
        }

        private string SegmentToFrameMinus(string segment, int value)
        {
            return
                "@" + value + Environment.NewLine +
                "D=A" + Environment.NewLine +
                FRAME + Environment.NewLine +
                "A=M-D" + Environment.NewLine +
                "D=M" + Environment.NewLine +
                segment + Environment.NewLine +
                "M=D" + Environment.NewLine;
        }
    }
}