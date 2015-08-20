using Base;
using System.Collections.Generic;
namespace PokemonRules
{
	public interface CharacterRules
	{
		IEnumerable<Move> levelUp(Pokemon charakter);
		void toLevel(Pokemon charakter, int level);
		Pokemon toPokemon(PKData data);
		Stats generateIV();
	}
}

