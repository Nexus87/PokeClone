using BattleLib.Components.BattleState;
using BattleLib.GraphicComponents;
using GameEngine.EventComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Events
{
    class SetPokemonEvent : IEvent
    {
        public event EventHandler OnEventProcessed = delegate { };
        
        private IBattleGraphicService graphic;
        private ClientIdentifier id;
        private PokemonWrapper pokemon;

        public SetPokemonEvent(IBattleGraphicService graphic, ClientIdentifier id, PokemonWrapper pokemon)
        {
            this.graphic = graphic;
            this.id = id;
            this.pokemon = pokemon;
        }

        private void PokemonSetHandler(object sender, EventArgs e)
        {
            graphic.OnPokemonSet -= PokemonSetHandler;
            OnEventProcessed(this, null);
        }

        public void Dispatch()
        {
            graphic.OnPokemonSet += PokemonSetHandler;
            graphic.SetPokemon(id, pokemon);
        }
    }
}
