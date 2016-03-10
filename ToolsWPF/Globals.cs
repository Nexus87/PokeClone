using Base;
using Base.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ToolsWPF
{
    static class Globals
    {
        public static IEnumerable<PokemonType> TypeList = Enum.GetValues(typeof(PokemonType)).Cast<PokemonType>();
    }
}
