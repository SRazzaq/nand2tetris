
namespace VMTranslator
{
    internal abstract class PushCommand : Command
    {
        public override string Code
        {
            get
            {
                return
                    LoadSegmentToD +
                    LoadDToStack +
                    IncrementStack;
            }
        }

        protected abstract string LoadSegmentToD
        {
            get;
        }
    }
}