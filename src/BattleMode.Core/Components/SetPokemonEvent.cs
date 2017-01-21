using System;
using Base.Rules;
using BattleMode.Graphic;
using BattleMode.Gui;
using BattleMode.Shared;
using GameEngine.Entities;

namespace BattleMode.Core.Components
{
    internal class SetPokemonEvent : IEvent
    {
        public event EventHandler EventProcessed = delegate { };

        private readonly IBattleGraphicController _graphic;
        private readonly IGuiController _guiController;
        private readonly ClientIdentifier _id;
        private readonly PokemonEntity _pokemon;

        public SetPokemonEvent(IBattleGraphicController graphic, IGuiController guiController, ClientIdentifier id, PokemonEntity pokemon)
        {
            _graphic = graphic;
            _guiController = guiController;
            _id = id;
            _pokemon = pokemon;
        }

        public void Dispatch()
        {
            _graphic.SetPokemon(_id, _pokemon);
            _guiController.SetPokemon(_id, _pokemon);
            EventProcessed(this, EventArgs.Empty);
        }
    }
}
