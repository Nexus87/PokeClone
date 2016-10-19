using Base;
using Base.Data;
using BattleLib;
using BattleLib.Components.BattleState;
using System.Collections.Generic;

namespace BattleLibTest.Utils
{
    public class TestFactory
    {
        public TestFactory()
        {
            PlayerID = new ClientIdentifier();
            AIID = new ClientIdentifier();
            BattleData = new BattleData(PlayerID, AIID);
        }

        public ClientIdentifier PlayerID { get; private set; }
        public ClientIdentifier AIID { get; private set; }
        public BattleData BattleData { get; private set; }
        
        public static Client CreatePlayerClient(int numPokemon)
        {
            var pokemons = new List<Pokemon>();
            for (int i = 0; i < numPokemon; i++)
            {
                pokemons.Add(CreatePokemon());
            }

            return new Client(new ClientIdentifier(), pokemons);
        }

        public void CreatePlayerPokemon(StatusCondition statusCondition = StatusCondition.Normal, int HP = 100)
        {
            BattleData.GetPokemon(PlayerID).Pokemon = CreatePokemon(statusCondition, HP);
        }

        public void CreateAIPokemon(StatusCondition statusCondition = StatusCondition.Normal, int HP = 100)
        {
            BattleData.GetPokemon(AIID).Pokemon = CreatePokemon(statusCondition, HP);
        }

        public static Pokemon CreatePokemon(StatusCondition statusCondition = StatusCondition.Normal, int HP = 100)
        {
            var baseData = new PokemonData();
            baseData.BaseStats = new Stats { HP = 100 };
            return new Pokemon(baseData, new Stats()) { Condition = statusCondition, HP = HP };
        }

        public Move CreateMove()
        {
            return new Move(new MoveData());
        }

        public Item CreateItem()
        {
            return new Item();
        }

        public Pokemon GetPlayerPokemon()
        {
            return BattleData.GetPokemon(PlayerID).Pokemon;
        }

        public Pokemon GetAIPokemon()
        {
            return BattleData.GetPokemon(AIID).Pokemon;
        }

        public void CreateAllPokemon(int HP = 100)
        {
            CreatePlayerPokemon(HP: HP);
            CreateAIPokemon(HP: HP);
        }
    }
}
