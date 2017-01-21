using System;
using Base.Rules;
using BattleMode.Shared;
using GameEngine.Entities;

namespace BattleMode.Gui
{
    public interface IGuiController : IGameEntity
    {
        event EventHandler TextDisplayed;
        event EventHandler MenuShowed;
        event EventHandler HpSet;

        void ShowMenu();
        void SetText(string text);
        void SetHp(ClientIdentifier target, int hp);
        void SetPokemon(ClientIdentifier id, PokemonEntity pokemon);
    }
}
