namespace Assembler
{
    public abstract class Command
    {
        private string mnemonic;

        public Command(string mnemonic)
        {
            this.mnemonic = mnemonic;
        }

        public string Mnemonic
        {
            get { return mnemonic; }
        }

        public abstract string Code
        {
            get;
        }
    }
}
