using System;
using GameEngine.Core.ECS.Systems;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;

namespace BattleMode.Gui
{
    public class MainMenuController
    {
        private readonly GuiSystem _guiSystem;

#pragma warning disable 649

        [GuiLoaderId("Grid")]
        private Grid _grid;
        [GuiLoaderId("Window")]
        private Window _window;

        [GuiLoaderId("attack")]
        private Button _attackButton;
        [GuiLoaderId("pkmn")]
        private Button _pkmnButton;
        [GuiLoaderId("item")]
        private Button _itemButton;
        [GuiLoaderId("run")]
        private Button _runButton;
#pragma warning restore 649

        public MainMenuController(GuiSystem guiSystem)
        {
            _guiSystem = guiSystem;
            _guiSystem.Factory.LoadFromFile(@"BattleMode\Gui\MainMenu.xml", this);

            _attackButton.ButtonPressed += delegate { OnItemSelected(MainMenuEntries.Attack); };
            _pkmnButton.ButtonPressed += delegate { OnItemSelected(MainMenuEntries.Pkmn); };
            _itemButton.ButtonPressed += delegate { OnItemSelected(MainMenuEntries.Item); };
            _runButton.ButtonPressed += delegate { OnItemSelected(MainMenuEntries.Run); };
            _window.SetInputListener(CommandKeys.Back, OnExitRequested);

        }

        public event EventHandler ExitRequested;
        public event EventHandler<SelectionEventArgs<MainMenuEntries>> ItemSelected;

        public void Show()
        {
            _grid.SelectComponent(0, 0);
            _guiSystem.ShowWidget(_window);
        }


        public void Close()
        {
            _guiSystem.CloseWidget(_window);
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