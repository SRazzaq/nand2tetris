namespace Assembler
{
    public class LCommand : Command
    {
        public LCommand(string mnemonic)
            : base(mnemonic)
        {
        }

        public override string Code
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
    }
}