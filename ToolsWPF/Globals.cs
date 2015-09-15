using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsWPF
{
    static class Globals
    {
        public static IEnumerable<PokemonType> TypeList = Enum.GetValues(typeof(PokemonType)).Cast<PokemonType>();
    }
}
