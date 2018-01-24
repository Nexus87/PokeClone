using System.Collections.Generic;
using System.Collections.ObjectModel;
using BattleMode.Gui.Actions;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;
using PokemonShared.Models;

namespace BattleMode.Gui
{
    public class ItemMenuController
    {
#pragma warning disable 649

        [GuiLoaderId("Window")]
        private Window _window;
        [GuiLoaderId("ListView")]
        private ListView<Item> _listView;

#pragma warning restore 649

        public ItemMenuController(GuiFactory factory, IMessageBus messageBus) 
        {
            factory.LoadFromFile(@"BattleMode\Gui\ItemMenu.xml", this);

            _listView.Model = new ObservableCollection<Item>();
            _listView.ListCellFactory = value =>
            {
                var button = new Button {Text = value.Name};
                return button;
            };

            _window.SetInputListener(CommandKeys.Back, () => messageBus.SendAction(new ShowMainMenuAction()));

        }

        public void Show(IMessageBus messageBus)
        {
            _listView.SelectCell(0);
            messageBus.SendAction(new SetGuiComponentVisibleAction(_window, true));
        }

        public void Close(IMessageBus messageBus)
        {
            messageBus.SendAction(new SetGuiComponentVisibleAction(_window, false));
        }

        public void SetItems(List<Item> trainerComponentItems)
        {
            _listView.Model.Clear();
            trainerComponentItems.ForEach(_listView.Model.Add);
        }
    }
}
