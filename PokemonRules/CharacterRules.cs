using Base;

namespace PokemonRules
{
	public interface CharacterRules
	{
		void levelUp(Pokemon charakter);
		void toLevel(Pokemon charakter, int level);
		Pokemon toPokemon(PKData data);
		Stats generateIV();
	}
}

