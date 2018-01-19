using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BattleMode.Shared;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
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
        private readonly IMessageBus _messageBus;


#pragma warning disable 649

        [GuiLoaderId("ListView")]
        private readonly ListView<Pokemon> _listView;
        [GuiLoaderId("Panel")]
        private readonly Panel _panel;

#pragma warning restore 649
        public PokemonMenuController(GuiSystem guiManager, Client client, GuiFactory factory, IMessageBus messageBus)
        {
            _guiManager = guiManager;
            _messageBus = messageBus;

            factory.LoadFromFile(@"BattleMode\Gui\PokemonMenu.xml", this);

            _panel.SetContent(_listView);

            _listView.Model = new ObservableCollection<Pokemon>(client.Pokemons);
            _listView.CellHeight = 75;
            _listView.ListCellFactory = value =>
            {
                var component = new SelectablePanel();
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
            _messageBus.SendAction(new SetGuiComponentVisibleAction { Widget = _panel, IsVisble = true });

        }

        public void Close()
        {
            _messageBus.SendAction(new SetGuiComponentVisibleAction { Widget = _panel, IsVisble = false });

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
