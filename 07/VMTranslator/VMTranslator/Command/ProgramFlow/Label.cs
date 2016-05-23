using System;
namespace VMTranslator
{
    internal class Label : Command
    {
        private string name;

        public Label(string name)
        {
            this.name = name;
        }

        public override string Code
        {
            get
            {
                return "(" + name + ")" + Environment.NewLine;
            }
        }
    }
}