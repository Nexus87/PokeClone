using BattleLib.Components.BattleState;
using BattleLib.GraphicComponents;
using GameEngine;
using System;

namespace BattleLib.Components
{
    class SetPokemonEvent : IEvent
    {
        public event EventHandler EventProcessed = delegate { };
        
        readonly IBattleGraphicController graphic;
        readonly ClientIdentifier id;
        readonly PokemonWrapper pokemon;

        public SetPokemonEvent(IBattleGraphicController graphic, ClientIdentifier id, PokemonWrapper pokemon)
        {
            this.graphic = graphic;
            this.id = id;
            this.pokemon = pokemon;
        }

        void PokemonSetHandler(object sender, EventArgs e)
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
