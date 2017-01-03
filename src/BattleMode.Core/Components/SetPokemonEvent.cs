using System;
using BattleMode.Core.Components.GraphicComponents;
using BattleMode.Entities.BattleState;
using BattleMode.Shared;
using GameEngine.Entities;

namespace BattleMode.Core.Components
{
    internal class SetPokemonEvent : IEvent
    {
        public event EventHandler EventProcessed = delegate { };

        private readonly IBattleGraphicController _graphic;
        private readonly ClientIdentifier _id;
        private readonly PokemonWrapper _pokemon;

        public SetPokemonEvent(IBattleGraphicController graphic, ClientIdentifier id, PokemonWrapper pokemon)
        {
            _graphic = graphic;
            _id = id;
            _pokemon = pokemon;
        }

        private void PokemonSetHandler(object sender, EventArgs e)
        {
            _graphic.PokemonSet -= PokemonSetHandler;
            EventProcessed(this, null);
        }

        public void Dispatch()
        {
            _graphic.PokemonSet += PokemonSetHandler;
            _graphic.SetPokemon(_id, _pokemon);
        }
    }
}
