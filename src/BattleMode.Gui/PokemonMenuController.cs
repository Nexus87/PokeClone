using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BattleMode.Shared;
using GameEngine.Core.ECS.Systems;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;
using GameEngine.GUI.Renderers;
using PokemonShared.Models;

namespace BattleMode.Gui
{
    public class PokemonMenuController
    {
        //private readonly List<PokemonMenuLine> _pokemonMenuLines = new List<PokemonMenuLine>();
        private readonly GuiSystem _guiManager;

#pragma warning disable 649

        [GuiLoaderId("ListView")]
        private readonly ListView<Pokemon> _listView;
        [GuiLoaderId("Panel")]
        private readonly Panel _panel;

#pragma warning restore 649
        public PokemonMenuController(GuiSystem guiManager, Client client, ISkin skin)
        {
            _guiManager = guiManager;

            var loader = new GuiLoader(@"BattleMode\Gui\PokemonMenu.xml") { Controller = this };
            loader.Load();

            _panel.SetContent(_listView);

            _listView.Model = new ObservableCollection<Pokemon>(client.Pokemons);
            _listView.CellHeight = 75;
            _listView.ListCellFactory = value =>
            {
                var component = new SelectablePanel((SelectablePanelRenderer)skin.GetRendererForComponent(typeof(SelectablePanel)));
                //var line = new PokemonMenuLine(); registry.ResolveType<PokemonMenuLine>();
                //component.Content = line;
                component.ShouldHandleKeyInput = true;
                component.PanelPressed += delegate { OnItemSelected(value); };
                //line.SetPokemon(value);
                //_pokemonMenuLines.Add(line);
                return component;
            };

            _panel.AddInputListener(CommandKeys.Back, OnExitRequested);

        }

        public event EventHandler ExitRequested;
        public event EventHandler<SelectionEventArgs<Pokemon>> ItemSelected;


        public void HandleKeyInput(CommandKeys key)
        {
            if (key == CommandKeys.Back)
                OnExitRequested();
            else
                _listView.HandleKeyInput(key);
        }

        public void Show()
        {
            _listView.SelectCell(0);
            //_pokemonMenuLines.ForEach(x => x.UpdateData());
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
    }
}
