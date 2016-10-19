using BattleLib.Components.BattleState;
using GameEngine;
using System;
using BattleLib.Components.GraphicComponents;
using GameEngine.GameEngineComponents;

namespace BattleLib.Components
{
    internal class SetPokemonEvent : IEvent
    {
        public event EventHandler EventProcessed = delegate { };

        private readonly IBattleGraphicController graphic;
        private readonly ClientIdentifier id;
        private readonly PokemonWrapper pokemon;

        public SetPokemonEvent(IBattleGraphicController graphic, ClientIdentifier id, PokemonWrapper pokemon)
        {
            this.graphic = graphic;
            this.id = id;
            this.pokemon = pokemon;
        }

        private void PokemonSetHandler(object sender, EventArgs e)
        {
            graphic.OnPokemonSet -= PokemonSetHandler;
            EventProcessed(this, null);
        }

        public void Dispatch()
        {
            graphic.OnPokemonSet += PokemonSetHandler;
            graphic.SetPokemon(id, pokemon);
        }
    }
}
