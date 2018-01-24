using System;
using BattleMode.Gui.Actions;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
using GameEngine.Globals;
using GameEngine.GUI;
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

#pragma warning restore 649
        private readonly IMessageBus _messageBus;

        public MainMenuController(GuiFactory factory, IMessageBus messageBus)
        {
            _messageBus = messageBus;
            factory.LoadFromFile(@"BattleMode\Gui\MainMenu.xml", this);

            _window.SetInputListener(CommandKeys.Back, OnExitRequested);

        }

        public event EventHandler ExitRequested;

        public void AttackSelected() => _messageBus.SendAction(new ShowMenuAction(MainMenuEntries.Attack));
        public void ItemSelected() => _messageBus.SendAction(new ShowMenuAction(MainMenuEntries.Item));
        public void PkmnSelected() => _messageBus.SendAction(new ShowMenuAction(MainMenuEntries.Pkmn));
        public void RunSelected() => _messageBus.SendAction(new ShowMenuAction(MainMenuEntries.Run));

        public void Show()
        {
            _grid.SelectComponent(0, 0);
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
    }
}