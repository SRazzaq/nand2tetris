using JackCompiler.AST;
using System.Collections.Generic;
using System.IO;

namespace JackCompiler.Parser
{
    public class ParameterParser : Parser
    {
        public ParameterParser(IEnumerator<Token> scanner)
            : base(scanner)
        {
        }

        public Parameter Parse()
        {
            var parameter = new Parameter();

            parameter.Type = GetToken<Token>().Value; //type
            parameter.Name = GetToken<IdentifierToken>().Value; //varName

            return parameter;
        }
    }
}
