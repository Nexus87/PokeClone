using System;
using System.Collections.ObjectModel;
using Base;
using BattleMode.Shared;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace BattleMode.Gui
{
    [GameType]
    public class PokemonMenuWidget : AbstractGraphicComponent, IMenuWidget<Pokemon>
    {
        private readonly ListView<Pokemon> _listView;
        private readonly Panel _panel;

        public PokemonMenuWidget(Client client, ListView<Pokemon> listView, Panel panel, IGameTypeRegistry registry)
        {
            _panel = panel;
            _listView = listView;

            _panel.SetContent(_listView);

            _listView.Model = new ObservableCollection<Pokemon>(client.Pokemons);
            _listView.CellHeight = 75;
            _listView.ListCellFactory = value =>
            {
                var component = registry.ResolveType<SelectablePanel>();
                var line = registry.ResolveType<PokemonMenuLine>();
                component.Content = line;
                component.Setup();
                line.SetPokemon(value);
                return component;
            };

        }

        public event EventHandler ExitRequested;
        public event EventHandler<SelectionEventArgs<Pokemon>> ItemSelected;

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _panel.Draw(time, batch);
        }

        protected override void Update()
        {
            _panel.SetCoordinates(this);
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
