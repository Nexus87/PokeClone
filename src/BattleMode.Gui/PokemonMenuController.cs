using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BattleMode.Gui.Actions;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;
using Microsoft.Xna.Framework;
using PokemonShared.Models;
using PokemonShared.Service;

namespace BattleMode.Gui
{
    public class PokemonMenuController
    {
        //private readonly List<PokemonMenuLine> _pokemonMenuLines = new List<PokemonMenuLine>();
        private readonly IMessageBus _messageBus;


#pragma warning disable 649

        [GuiLoaderId("ListView")]
        private readonly ListView<Pokemon> _listView;
        [GuiLoaderId("Panel")]
        private readonly Panel _panel;

#pragma warning restore 649
        public PokemonMenuController(GuiFactory factory, IMessageBus messageBus, SpriteProvider spriteProvider)
        {
            _messageBus = messageBus;

            factory.LoadFromFile(@"BattleMode\Gui\PokemonMenu.xml", this);

            _panel.SetContent(_listView);

            _listView.Model = new ObservableCollection<Pokemon>();
            _listView.ListCellFactory = value =>
            {
                var line = new PokemonMenuLineController(factory, spriteProvider);
                line.SetPokemon(value);

                var component = new SelectablePanel {
                    ShouldHandleKeyInput = true,
                    Content = line.Component
                };
                component.PanelPressed += delegate { OnItemSelected(value); };
                return component;
            };

            _panel.AddInputListener(CommandKeys.Back, () => messageBus.SendAction(new ShowMainMenuAction()));

        }

        public event EventHandler ExitRequested;
        public event EventHandler<SelectionEventArgs<Pokemon>> ItemSelected;

        public void SetPlayerPokemon(List<Pokemon> pokemon)
        {
            _listView.Model.Clear();
            pokemon.ForEach(_listView.Model.Add);
        }

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
            _messageBus.SendAction(new SetGuiComponentVisibleAction(_panel, true));

        }

        public void Close()
        {
            _messageBus.SendAction(new SetGuiComponentVisibleAction(_panel, false));

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
