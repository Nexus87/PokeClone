using System;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace BattleMode.Gui
{
    [GameType]
    public class MainMenuWidget : AbstractGraphicComponent, IMenuWidget<MainMenuEntries>
    {
        private readonly Grid _grid;
        private readonly Window _window;

        public MainMenuWidget(Window window, Grid grid, Button attackButton, Button pkmnButton, Button itemButton, Button runButton)
        {
            _window = window;
            _window.SetContent(grid);

            _grid = grid;
            _grid.AddPercentColumn();
            _grid.AddPercentColumn();
            _grid.AddPercentRow();
            _grid.AddPercentRow();

            attackButton.Text = MainMenuEntries.Attack.ToString();
            attackButton.ButtonPressed += delegate { OnItemSelected(MainMenuEntries.Attack); };

            pkmnButton.Text = MainMenuEntries.Pkmn.ToString();
            pkmnButton.ButtonPressed += delegate { OnItemSelected(MainMenuEntries.Pkmn); };

            itemButton.Text = MainMenuEntries.Item.ToString();
            itemButton.ButtonPressed += delegate { OnItemSelected(MainMenuEntries.Item); };

            runButton.Text = MainMenuEntries.Run.ToString();
            runButton.ButtonPressed += delegate { OnItemSelected(MainMenuEntries.Run); };

            _grid.SetComponent(attackButton, 0, 0);
            _grid.SetComponent(pkmnButton, 0, 1);
            _grid.SetComponent(itemButton, 1, 0);
            _grid.SetComponent(runButton, 1, 1);
        }

        public event EventHandler ExitRequested;
        public event EventHandler<SelectionEventArgs<MainMenuEntries>> ItemSelected;

        public void ResetSelection()
        {
            _grid.SelectComponent(0, 0);
        }

        public override void Setup()
        {
            _window.Setup();
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
            if(key == CommandKeys.Back)
                OnExitRequested();
            else
                _window.HandleKeyInput(key);
        }

        protected virtual void OnItemSelected(MainMenuEntries e)
        {
            ItemSelected?.Invoke(this, new SelectionEventArgs<MainMenuEntries>(e));
        }

        protected virtual void OnExitRequested()
        {
            ExitRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}