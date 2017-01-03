using System;
using GameEngine.Core;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;

namespace BattleMode.Gui
{
    [GameType]
    public class MainMenuController
    {
        private readonly GuiManager _guiManager;

#pragma warning disable 649

        [GuiLoaderId("Grid")]
        private Grid _grid;
        [GuiLoaderId("Window")]
        private Window _window;

#pragma warning restore 649

        public MainMenuController(GuiManager guiManager, Button attackButton, Button pkmnButton, Button itemButton, Button runButton)
        {
            _guiManager = guiManager;
            var loader = new GuiLoader(@"BattleMode\Gui\MainMenu.xml") {Controller = this};

            loader.Load();

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

            _window.SetInputListener(CommandKeys.Back, OnExitRequested);

        }

        public event EventHandler ExitRequested;
        public event EventHandler<SelectionEventArgs<MainMenuEntries>> ItemSelected;

        public void Show()
        {
            _grid.SelectComponent(0, 0);
            _guiManager.ShowWidget(_window);
        }


        public void Close()
        {
            _guiManager.CloseWidget(_window);
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