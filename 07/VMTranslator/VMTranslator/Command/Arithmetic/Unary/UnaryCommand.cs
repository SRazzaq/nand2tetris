namespace VMTranslator
{
    abstract class UnaryCommand : Command
    {
        public override string Code
        {
            get
            {
                return
                    DecrementStack +
                    Command +
                    IncrementStack;
            }
        }

        public abstract string Command
        {
            get;
        }
    }
}
