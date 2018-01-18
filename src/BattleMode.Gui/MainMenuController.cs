using System;
using GameEngine.Core.ECS.Systems;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;

namespace BattleMode.Gui
{
    public class MainMenuController
    {
        private readonly GuiSystem _guiManager;

#pragma warning disable 649

        [GuiLoaderId("Grid")]
        private Grid _grid;
        [GuiLoaderId("Window")]
        private Window _window;

#pragma warning restore 649

        public MainMenuController(GuiSystem guiManager, Button attackButton, Button pkmnButton, Button itemButton, Button runButton, GuiFactory factory)
        {
            _guiManager = guiManager;

            factory.LoadFromFile(@"BattleMode\Gui\MainMenu.xml", this);

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