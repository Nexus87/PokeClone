using System.Collections.Generic;
using Base;
using Base.Data;
using Base.Rules;
using BattleMode.Entities.BattleState;
using BattleMode.Shared;

namespace BattleModeTest.Utils
{
    public class TestFactory
    {
        public TestFactory()
        {
            PlayerId = new ClientIdentifier();
            Aiid = new ClientIdentifier();
            BattleData = new BattleData(PlayerId, Aiid);
        }

        public ClientIdentifier PlayerId { get; }
        public ClientIdentifier Aiid { get; }
        public BattleData BattleData { get; }
        
        public static Client CreatePlayerClient(int numPokemon)
        {
            var pokemons = new List<Pokemon>();
            for (var i = 0; i < numPokemon; i++)
            {
                pokemons.Add(CreatePokemon());
            }

            return new Client(new ClientIdentifier(), pokemons);
        }

        public void CreatePlayerPokemon(StatusCondition statusCondition = StatusCondition.Normal, int hp = 100)
        {
            BattleData.GetPokemon(PlayerId).Pokemon = CreatePokemon(statusCondition, hp);
        }

        public void CreateAiPokemon(StatusCondition statusCondition = StatusCondition.Normal, int hp = 100)
        {
            BattleData.GetPokemon(Aiid).Pokemon = CreatePokemon(statusCondition, hp);
        }

        public static Pokemon CreatePokemon(StatusCondition statusCondition = StatusCondition.Normal, int hp = 100)
        {
            var baseData = new PokemonData {BaseStats = new Stats {Hp = 100}};
            return new Pokemon(baseData, new Stats()) { Condition = statusCondition, Hp = hp };
        }

        public Move CreateMove()
        {
            return new Move(new MoveData());
        }

        public Item CreateItem()
        {
            return new Item();
        }

        public PokemonEntity GetPlayerPokemon()
        {
            return BattleData.GetPokemon(PlayerId);
        }

        public PokemonEntity GetAiPokemon()
        {
            return BattleData.GetPokemon(Aiid);
        }

        public void CreateAllPokemon(int hp = 100)
        {
            CreatePlayerPokemon(hp: hp);
            CreateAiPokemon(hp: hp);
        }
    }
}
