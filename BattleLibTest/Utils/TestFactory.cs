using Base;
using Base.Data;
using BattleLib;
using BattleLib.Components.BattleState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void CreatePlayerPokemon(StatusCondition statusCondition = StatusCondition.Normal, int HP = 100)
        {
            BattleData.GetPokemon(PlayerID).Pokemon = CreatePokemon(statusCondition, HP);
        }

        public void CreateAIPokemon(StatusCondition statusCondition = StatusCondition.Normal, int HP = 100)
        {
            BattleData.GetPokemon(AIID).Pokemon = CreatePokemon(statusCondition, HP);
        }

        public Pokemon CreatePokemon(StatusCondition statusCondition = StatusCondition.Normal, int HP = 100)
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
