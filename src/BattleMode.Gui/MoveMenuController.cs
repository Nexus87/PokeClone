using System;
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
    public class MoveMenuController
    {
        private readonly GuiSystem _guiManager;
        private readonly ISkin _skin;

#pragma warning disable 649

        [GuiLoaderId("Window")]
        private readonly Window _window;
        [GuiLoaderId("ListView")]
        private readonly ListView<Move> _listView;

#pragma warning restore 649

        public MoveMenuController(GuiSystem guiManager, BattleData data, ISkin skin, GuiFactory factory)
        {
            _guiManager = guiManager;
            _skin = skin;

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
                var button = new Button((ButtonRenderer) _skin.GetRendererForComponent(typeof(Button)));
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
            _guiManager.ShowWidget(_window);
            _listView.SelectCell(0);
        }

        public void Close()
        {
            _guiManager.CloseWidget(_window);
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