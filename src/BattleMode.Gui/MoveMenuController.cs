using System;
using Base;
using BattleMode.Entities.BattleState;
using GameEngine.Core;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;

namespace BattleMode.Gui
{
    [GameType]
    public class MoveMenuController
    {
        private readonly GuiManager _guiManager;
        private readonly Window _window;
        private readonly ListView<Move> _listView;

        public MoveMenuController(GuiManager guiManager, ScreenConstants screenConstants, BattleData data, Window window, ListView<Move> listView, IGameTypeRegistry registry)
        {
            _guiManager = guiManager;
            _window = window;
            _listView = listView;
            _listView.CellHeight = 50;

            _listView.Model = data.GetPokemon(data.PlayerId).Moves;
            _listView.ListCellFactory = value =>
            {
                if (value == null)
                {
                    var label = registry.ResolveType<Label>();
                    label.Text = " --------";
                    return label;
                }
                else
                {
                    var button = registry.ResolveType<Button>();
                    button.Text = value.Name;
                    button.ButtonPressed += delegate { OnItemSelected(value); };
                    return button;
                }

            };

            _window.SetContent(_listView);
            InitWindow(screenConstants);
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

        private void InitWindow(ScreenConstants screen)
        {
            _window.SetCoordinates(
                screen.ScreenWidth / 2.0f,
                2.0f * screen.ScreenHeight / 3.0f,
                width: screen.ScreenWidth - screen.ScreenWidth / 2.0f,
                height: screen.ScreenHeight - 2.0f * screen.ScreenHeight / 3.0f
            );

            _window.SetInputListener(CommandKeys.Back, OnExitRequested);
        }


    }
}