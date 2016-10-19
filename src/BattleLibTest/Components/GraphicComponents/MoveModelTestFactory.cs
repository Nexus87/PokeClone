using Base;
using Base.Data;
using BattleLib;
using BattleLib.Components.BattleState;
using BattleLib.Components.GraphicComponents;

namespace BattleLibTest.GUI
{
    internal class MoveModelTestFactory
    {
        public static Pokemon CreatePokemon(int numMoves)
        {
            var pkmn = new Pokemon(new PokemonData(), new Stats());
            for (int i = 0; i < numMoves; i++)
                pkmn.SetMove(i, new Move(new MoveData { Name = GetMoveName(i) }));

            return pkmn;
        }

        public static MoveModel CreateModel(int moves)
        {
            return CreateModel(CreatePokemon(moves));

        }
        public static MoveModel CreateEmptyModel()
        {
            return CreateModel(pkmn: null);
        }

        public static MoveModel CreateModel(Pokemon pkmn)
        {
            var wrapper = new PokemonWrapper(new ClientIdentifier());
            if(pkmn != null)
                wrapper.Pokemon = pkmn;

            return CreateModel(wrapper);
        }

        public static MoveModel CreateModel(PokemonWrapper wrapper)
        {
            return new MoveModel(wrapper);
        }

        public static PokemonWrapper CreateWrapper()
        {
            return new PokemonWrapper(new ClientIdentifier());
        }

        public static string GetMoveName(int number)
        {
            return "Move " + number;
        }
    }
}