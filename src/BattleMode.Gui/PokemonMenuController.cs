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

#pragma warning disable 649

        [GuiLoaderId("ListView")]
        private readonly ListView<Pokemon> _listView;
        [GuiLoaderId("Panel")]
        private readonly Panel _panel;

#pragma warning restore 649
        public PokemonMenuController(GuiFactory factory, IMessageBus messageBus, SpriteProvider spriteProvider)
        {

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
                return component;
            };

            _panel.AddInputListener(CommandKeys.Back, () => messageBus.SendAction(new ShowMainMenuAction()));

        }

        public event EventHandler ExitRequested;

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

        public void Show(IMessageBus messageBus)
        {
            _listView.SelectCell(0);
            messageBus.SendAction(new SetGuiComponentVisibleAction(_panel, true));

        }

        public void Close(IMessageBus messageBus)
        {
            messageBus.SendAction(new SetGuiComponentVisibleAction(_panel, false));

        }
        protected void OnExitRequested()
        {
            ExitRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
