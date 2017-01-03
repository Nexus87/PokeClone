using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Base;
using BattleMode.Shared;
using GameEngine.Core;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;

namespace BattleMode.Gui
{
    [GameType]
    public class PokemonMenuController
    {
        private readonly ListView<Pokemon> _listView;
        private readonly List<PokemonMenuLine> _pokemonMenuLines = new List<PokemonMenuLine>();
        private readonly GuiManager _guiManager;
        private readonly Panel _panel;

        public PokemonMenuController(GuiManager guiManager, ScreenConstants screenConstants, Client client, ListView<Pokemon> listView, Panel panel, IGameTypeRegistry registry)
        {
            _guiManager = guiManager;
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
                component.ShouldHandleKeyInput = true;
                component.PanelPressed += delegate { OnItemSelected(value); };
                line.SetPokemon(value);
                _pokemonMenuLines.Add(line);
                return component;
            };

            InitPanel(screenConstants);

        }

        public event EventHandler ExitRequested;
        public event EventHandler<SelectionEventArgs<Pokemon>> ItemSelected;


        public void HandleKeyInput(CommandKeys key)
        {
            if(key == CommandKeys.Back)
                OnExitRequested();
            else
                _listView.HandleKeyInput(key);
        }

        public void Show()
        {
            _listView.SelectCell(0);
            _pokemonMenuLines.ForEach(x => x.UpdateData());
            _guiManager.ShowWidget(_panel);
        }

        public void Close()
        {
            _guiManager.CloseWidget(_panel);
        }
        protected void OnExitRequested()
        {
            ExitRequested?.Invoke(this, EventArgs.Empty);
        }

        protected void OnItemSelected(Pokemon p)
        {
            ItemSelected?.Invoke(this, new SelectionEventArgs<Pokemon>(p));
        }

        private void InitPanel(ScreenConstants screen)
        {
            const int xPosition = 0;
            const int yPosition = 0;
            var width = screen.ScreenWidth;
            var height = 2.0f * screen.ScreenHeight / 3.0f;

            _panel.SetCoordinates(xPosition, yPosition, width, height);
            _panel.AddInputListener(CommandKeys.Back, OnExitRequested);
        }
    }
}
