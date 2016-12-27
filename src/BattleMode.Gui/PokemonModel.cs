using System;
using System.Collections.Generic;
using System.Linq;
using Base;
using BattleMode.Components.BattleState;
using BattleMode.Shared;
using GameEngine.GUI.Graphics.TableView;
using GameEngine.TypeRegistry;

namespace BattleMode.Gui
{
    [GameType]
    public class PokemonModel : ITableModel<Pokemon>
    {
        public PokemonModel(Client client, BattleData data) :
            this(client, data.GetPokemon(client.Id))
        {}

        internal PokemonModel(Client client, PokemonWrapper playerPokemon)
        {
            pokemons = client.Pokemons;
            playerPokemon.PokemonValuesChanged += PokemonValueChangedHandler;
        }

        private void PokemonValueChangedHandler(object sender, PokemonChangedEventArgs e)
        {
            var index = pokemons.ToList().IndexOf(e.Pokemon);
            if (index == -1)
                return;

            DataChanged(this, new DataChangedEventArgs<Pokemon>(index, 0, e.Pokemon));
        }

        public event EventHandler<DataChangedEventArgs<Pokemon>> DataChanged;
        public event EventHandler<TableResizeEventArgs> SizeChanged
        {
            add {  }
            remove { }
        }

        private IReadOnlyList<Pokemon> pokemons;

        public int Rows
        {
            get { return pokemons.Count; }
        }

        public int Columns
        {
            get { return 1; }
        }

        public Pokemon DataAt(int row, int column)
        {
            if (column > 0)
                return null;
            return row >= pokemons.Count ? null : pokemons[row];
        }

        public bool SetDataAt(Pokemon data, int row, int column)
        {
            throw new InvalidOperationException("Error: model is read only");
        }
    }
}
