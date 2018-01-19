using System;
using System.Collections.ObjectModel;
using BattleMode.Shared;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;
using PokemonShared.Models;

namespace BattleMode.Gui
{
    public class MoveMenuController
    {
        private readonly IMessageBus _messageBus;

        public MoveMenuController(BattleData data, GuiFactory factory, IMessageBus messageBus)
        {
            _messageBus = messageBus;

            factory.LoadFromFile(@"BattleMode\Gui\MoveMenu.xml", this);

            _listView.Model = new ObservableCollection<Move>(data.GetPokemon(data.PlayerId).Moves);

            data.GetPokemon(data.PlayerId).PokemonChanged += (sender, args) =>
            {
                _listView.Model.Clear();
                foreach (var sourceMove in args.Source.Moves)
                    _listView.Model.Add(sourceMove);
            };


            _listView.ListCellFactory = value =>
            {
                var button = new Button
                {
                    Text = value?.Name ?? "--------",
                    Enabled = value != null
                };
                button.ButtonPressed += delegate { OnItemSelected(value); };
                return button;
            };

            _window.SetInputListener(CommandKeys.Back, OnExitRequested);
        }

        public event EventHandler ExitRequested;
        public event EventHandler<SelectionEventArgs<Move>> ItemSelected;

        public void Show()
        {
            _listView.SelectCell(0);
            _messageBus.SendAction(new SetGuiComponentVisibleAction(_window, true));
        }

        public void Close()
        {
            _messageBus.SendAction(new SetGuiComponentVisibleAction(_window, false));
        }

        protected virtual void OnExitRequested()
        {
            ExitRequested?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnItemSelected(Move m)
        {
            ItemSelected?.Invoke(this, new SelectionEventArgs<Move>(m));
        }

#pragma warning disable 649

        [GuiLoaderId("Window")] private readonly Window _window;

        [GuiLoaderId("ListView")] private readonly ListView<Move> _listView;

#pragma warning restore 649
    }
}