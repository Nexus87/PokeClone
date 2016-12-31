using System;
using Base;
using BattleMode.Components.BattleState;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.General;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace BattleMode.Gui
{
    [GameType]
    public class MoveMenuWidget : AbstractGraphicComponent, IMenuWidget<Move>
    {
        private readonly Window _window;
        private readonly ListView<Move> _listView;

        public MoveMenuWidget(BattleData data, Window window, ListView<Move> listView, IGameTypeRegistry registry)
        {
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
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _window.Draw(time, batch);
        }

        protected override void Update()
        {
            _window.SetCoordinates(this);
        }

        public override void HandleKeyInput(CommandKeys key)
        {
            if (key == CommandKeys.Back)
                OnExitRequested();
            _window.HandleKeyInput(key);
        }

        public event EventHandler ExitRequested;
        public event EventHandler<SelectionEventArgs<Move>> ItemSelected;

        public void ResetSelection()
        {
            _listView.SelectCell(0);
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