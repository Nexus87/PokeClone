using Base;
using System.Collections.Generic;
namespace PokemonRules
{
	public interface ICharacterRules
	{
		IEnumerable<Move> LevelUp(Pokemon character);
		void ToLevel(Pokemon character, int level);
		Pokemon ToPokemon(PKData data);
		Stats GenerateIV();
	}
}

