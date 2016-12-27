using System.Linq;
using BattleMode.Components.BattleState;
using BattleMode.Core.Components.GraphicComponents;
using BattleMode.Gui;
using BattleMode.Shared;
using BattleModeTest.Utils;
using NUnit.Framework;

namespace BattleModeTest.Components.GraphicComponents
{
    [TestFixture]
    public class PokemonModelTest
    {

        [TestCase(6, 6)]
        [TestCase(1, 1)]
        public void Rows_NormalSetup_ReturnsGivenNumberOfPokemon(int expectedRows, int numberOfPokemon)
        {
            var pokemonModel = CreateModel(numberOfPokemon);

            Assert.AreEqual(expectedRows, pokemonModel.Rows);
        }

        [TestCase]
        public void DataChanged_HpOfPokemonChanged_EventIsRaised()
        {
            int numberOfPokemon = 5;
            var client = TestFactory.CreatePlayerClient(numberOfPokemon);
            var pokemonWrapper = new PokemonWrapper(client.Id) { Pokemon = client.Pokemons.First() };
            var pokemonModel = CreateModel(client, pokemonWrapper);
            bool eventWasRaised = false;

            pokemonModel.DataChanged += delegate { eventWasRaised = true; };
            pokemonWrapper.HP = 0;

            Assert.True(eventWasRaised);
        }

        private PokemonModel CreateModel(Client client, PokemonWrapper pokemon = null)
        {
            if (pokemon == null)
                pokemon = new PokemonWrapper(client.Id);
            return new PokemonModel(client, pokemon);
        }
        private PokemonModel CreateModel(int numberOfPokemon)
        {
            return CreateModel(TestFactory.CreatePlayerClient(numberOfPokemon));
        }
    }
}
