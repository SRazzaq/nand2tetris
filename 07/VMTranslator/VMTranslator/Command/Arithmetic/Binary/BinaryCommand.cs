
namespace VMTranslator
{
    abstract class BinaryCommand : Command
    {
        public override string Code
        {
            get
            {
                return
                    DecrementStack +
                    LoadStackToD +
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
