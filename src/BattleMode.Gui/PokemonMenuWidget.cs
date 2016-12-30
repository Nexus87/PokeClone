using System;
using System.Collections.ObjectModel;
using Base;
using BattleMode.Shared;
using GameEngine.Core;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Graphics.GUI;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace BattleMode.Gui
{
    [GameType]
    public class PokemonMenuWidget : AbstractGraphicComponent, IMenuWidget<Pokemon>
    {
        private readonly Dialog _dialog;
        private readonly ListView<Pokemon> _listView;

        public PokemonMenuWidget(Client client, ListView<Pokemon> listView, Pixel pixel, ScreenConstants constants, IGameTypeRegistry registry)
        {
            pixel.Color = constants.BackgroundColor;
            _dialog = new Dialog(pixel);
            _listView = listView;

            _listView.Model = new ObservableCollection<Pokemon>(client.Pokemons);
            _listView.ListCellFactory = value =>
            {
                var component = registry.ResolveType<SelectableContainer<PokemonMenuLine>>();
                component.Content = registry.ResolveType<PokemonMenuLine>();
                component.Setup();
                component.Content.SetPokemon(value);
                return component;
            };

            _dialog.AddWidget(_listView);
        }

        public event EventHandler ExitRequested;
        public event EventHandler<SelectionEventArgs<Pokemon>> ItemSelected;

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _dialog.Draw(time, batch);
        }

        protected override void Update()
        {
            _dialog.SetCoordinates(this);
        }

        public override void HandleKeyInput(CommandKeys key)
        {
            if(key == CommandKeys.Back)
                OnExitRequested();
            else
                _listView.HandleKeyInput(key);
        }

        public void ResetSelection()
        {
            _listView.SelectCell(0);
        }

        protected virtual void OnExitRequested()
        {
            ExitRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
