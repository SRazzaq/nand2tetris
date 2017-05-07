
namespace JackCompiler
{
    public enum Kind
    {
        STATIC,
        FIELD,
        ARG,
        VAR
    };

    public class Symbol
    {
        public Symbol(string type, Kind kind, int index)
        {
            Type = type;
            Kind = kind;
            Index = index;
        }

        public string Type { get; private set; }
        public Kind Kind { get; private set; }
        public int Index { get; private set; }
    }
}
