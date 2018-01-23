using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly IMessageBus _messageBus;
#pragma warning disable 649

        [GuiLoaderId("Window")]
        private Window _window;
        [GuiLoaderId("ListView")]
        private ListView<Item> _listView;

#pragma warning restore 649

        public ItemMenuController(GuiFactory factory, IMessageBus messageBus) 
        {
            _messageBus = messageBus;
            factory.LoadFromFile(@"BattleMode\Gui\ItemMenu.xml", this);

            _listView.Model = new ObservableCollection<Item>();
            _listView.ListCellFactory = value =>
            {
                var button = new Button {Text = value.Name};
                button.OnPressed += delegate { OnItemSelected(value); };
                return button;
            };

            _window.SetInputListener(CommandKeys.Back, OnExitRequested);

        }

        public event EventHandler ExitRequested;
        public event EventHandler<SelectionEventArgs<Item>> ItemSelected;

        public void Show()
        {
            _listView.SelectCell(0);
            _messageBus.SendAction(new SetGuiComponentVisibleAction(_window, true));
        }

        public void Close()
        {
            _messageBus.SendAction(new SetGuiComponentVisibleAction(_window, false));
        }

        protected virtual void OnItemSelected(Item i)
        {
            ItemSelected?.Invoke(this, new SelectionEventArgs<Item>(i));
        }

        protected void OnExitRequested()
        {
            ExitRequested?.Invoke(this, EventArgs.Empty);
        }

        public void SetItems(List<Item> trainerComponentItems)
        {
            _listView.Model.Clear();
            trainerComponentItems.ForEach(_listView.Model.Add);
        }
    }
}
