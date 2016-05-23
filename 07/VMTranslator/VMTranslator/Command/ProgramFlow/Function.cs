using System;

namespace VMTranslator
{
    internal class Function : Command
    {
        private string functionName;
        private string localVariableCount;

        public Function(string functionName, string localVariableCount)
        {
            this.functionName = functionName;
            this.localVariableCount = localVariableCount;
        }

        public override string Code
        {
            get
            {
                var pushLocalVariable = string.Empty;
                for (int i = 0; i < int.Parse(localVariableCount); i++)
                {
                    pushLocalVariable += new PushConstant("0").Code;
                }

                return
                    "(" + functionName + ")" + Environment.NewLine +
                    pushLocalVariable;
            }
        }
    }
}