using Base;
using Base.Data;

namespace PokemonRulesTest
{
    static class Extensions {
        public static bool compare(this PokemonData d, object obj) {
            if (obj == null || !(obj is PokemonData))
                return false;

            var other = (PokemonData)obj;
            return d.Name.Equals(other.Name) &&
                   d.Id == other.Id &&
                   d.BaseStats.HP == other.BaseStats.HP &&
                   d.BaseStats.Atk == other.BaseStats.Atk &&
                   d.BaseStats.Def == other.BaseStats.Def &&
                   d.BaseStats.SpAtk == other.BaseStats.SpAtk &&
                   d.BaseStats.SpDef == other.BaseStats.SpDef &&
                   d.BaseStats.Speed == other.BaseStats.Speed &&
                   d.Type1 == other.Type1 &&
                   d.Type2 == other.Type2;
        }

        public static bool compare(this Pokemon p, object obj) {
            var data = obj as PokemonData;
            if (data == null)
                return false;
			
            return p.Atk == data.BaseStats.Atk &&
                   p.Def == data.BaseStats.Def &&
                   p.SpAtk == data.BaseStats.SpAtk &&
                   p.SpDef == data.BaseStats.SpDef &&
                   p.Speed == data.BaseStats.Speed &&
                   p.HP == data.BaseStats.HP &&
                   p.Name == data.Name &&
                   p.Type1 == data.Type1 &&
                   p.Type2 == data.Type2;
        }
    }
}