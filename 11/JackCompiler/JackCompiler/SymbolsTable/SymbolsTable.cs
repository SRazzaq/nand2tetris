using System.Collections.Generic;
using System.Linq;

namespace JackCompiler
{
    public class SymbolsTable
    {
        private Dictionary<string, Symbol> ClassScope = new Dictionary<string, Symbol>();
        private Dictionary<string, Symbol> MethodScope = new Dictionary<string, Symbol>();

        public void StartSubroutine()
        {
            MethodScope.Clear();
        }

        public void Define(string name, string type, Kind kind)
        {
            if (kind == Kind.ARG || kind == Kind.VAR)
            {
                MethodScope.Add(name, new Symbol(type, kind, VarCount(kind)));
            }
            else
            {
                ClassScope.Add(name, new Symbol(type, kind, VarCount(kind)));
            }
        }

        public int VarCount(Kind kind)
        {
            if (kind == Kind.ARG || kind == Kind.VAR)
            {
                return MethodScope.Count(x => x.Value.Kind == kind);
            }
            else
            {
                return ClassScope.Count(x => x.Value.Kind == kind);
            }
        }

        public Kind KindOf(string name)
        {
            if (ClassScope.ContainsKey(name))
                return ClassScope[name].Kind;

            return MethodScope[name].Kind;
        }

        public string TypeOf(string name)
        {
            if (ClassScope.ContainsKey(name))
                return ClassScope[name].Type;

            if (MethodScope.ContainsKey(name))
                return MethodScope[name].Type;

            return null;
        }

        public int IndexOf(string name)
        {
            if (ClassScope.ContainsKey(name))
                return ClassScope[name].Index;

            return MethodScope[name].Index;
        }
    }
}
