using System;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
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
        private readonly IMessageBus _messageBus;

        public MainMenuController(GuiFactory factory, IMessageBus messageBus)
        {
            _messageBus = messageBus;
            factory.LoadFromFile(@"BattleMode\Gui\MainMenu.xml", this);

            _attackButton.OnPressed += delegate { OnItemSelected(MainMenuEntries.Attack); };
            _pkmnButton.OnPressed += delegate { OnItemSelected(MainMenuEntries.Pkmn); };
            _itemButton.OnPressed += delegate { OnItemSelected(MainMenuEntries.Item); };
            _runButton.OnPressed += delegate { OnItemSelected(MainMenuEntries.Run); };
            _window.SetInputListener(CommandKeys.Back, OnExitRequested);

        }

        public event EventHandler ExitRequested;
        public event EventHandler<SelectionEventArgs<MainMenuEntries>> ItemSelected;
        public void Write() {
            System.Console.WriteLine("Run!");
        }
        public void Show()
        {
            _grid.SelectComponent(0, 0);
            _messageBus.SendAction(new SetGuiComponentVisibleAction(_window, true));
        }


        public void Close()
        {
            _messageBus.SendAction(new SetGuiComponentVisibleAction(_window, false));
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