using System;
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
    public class MoveMenuController
    {
        private readonly GuiSystem _guiManager;
        private readonly IMessageBus _messageBus;

#pragma warning disable 649

        [GuiLoaderId("Window")]
        private readonly Window _window;
        [GuiLoaderId("ListView")]
        private readonly ListView<Move> _listView;

#pragma warning restore 649

        public MoveMenuController(GuiSystem guiManager, BattleData data, GuiFactory factory, IMessageBus messageBus)
        {
            _guiManager = guiManager;
            _messageBus = messageBus;

            factory.LoadFromFile(@"BattleMode\Gui\MoveMenu.xml", this);

            _listView.Model = new ObservableCollection<Move>(data.GetPokemon(data.PlayerId).Moves);

            data.GetPokemon(data.PlayerId).PokemonChanged += (sender, args) =>
            {
                _listView.Model.Clear();
                foreach (var sourceMove in args.Source.Moves)
                {
                    _listView.Model.Add(sourceMove);
                }
            };


            _listView.ListCellFactory = value =>
            {
                var button = new Button();
                button.Text = value?.Name ?? "--------";
                button.Enabled = value != null;
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
            _messageBus.SendAction(new SetGuiComponentVisibleAction { Widget = _window, IsVisble = true });

        }

        public void Close()
        {
            _messageBus.SendAction(new SetGuiComponentVisibleAction { Widget = _window, IsVisble = false });

        }
        protected virtual void OnExitRequested()
        {
            ExitRequested?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnItemSelected(Move m)
        {
            ItemSelected?.Invoke(this, new SelectionEventArgs<Move>(m));
        }

    }
}