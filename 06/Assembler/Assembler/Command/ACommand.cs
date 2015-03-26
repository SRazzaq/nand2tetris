using System;

namespace Assembler
{
    public class ACommand : Command
    {
        public ACommand(string mnemonic)
            : base(mnemonic)
        {
        }

        public override string Code
        {
            get
            {
                return string.Format("0{0}", Address);
            }
        }

        private string Address
        {
            get
            {
                var symbol = Mnemonic.TrimStart('@');

                int address;
                if (!int.TryParse(symbol, out address))
                {
                    address = SymbolTable.GetAddress(symbol);
                }

                return Convert.ToString(address, 2).PadLeft(15, '0'); ;
            }
        }
    }
}
