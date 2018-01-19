using System;
using System.Collections.ObjectModel;
using System.Linq;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Systems;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;
using GameEngine.GUI.Renderers.PokemonClassicRenderer;
using PokemonShared.Models;

namespace BattleMode.Gui
{
    public class ItemMenuController
    {
        private readonly GuiSystem _guiManager;
        private readonly IMessageBus _messageBus;
#pragma warning disable 649

        [GuiLoaderId("Window")]
        private Window _window;
        [GuiLoaderId("ListView")]
        private ListView<Item> _listView;

#pragma warning restore 649

        public ItemMenuController(GuiSystem guiManager, GuiFactory factory, IMessageBus messageBus) 
        {
            _guiManager = guiManager;
            _messageBus = messageBus;
            factory.LoadFromFile(@"BattleMode\Gui\ItemMenu.xml", this);

            var model = Enumerable
                .Range(0, 20)
                .Select(x => new Item {Name = "Item" + x});

            _listView.Model = new ObservableCollection<Item>(model);
            _listView.ListCellFactory = value =>
            {
                var button = new Button();
                button.Text = value.Name;
                button.ButtonPressed += delegate { OnItemSelected(value); };
                return button;
            };

            _window.SetInputListener(CommandKeys.Back, OnExitRequested);

        }

        public event EventHandler ExitRequested;
        public event EventHandler<SelectionEventArgs<Item>> ItemSelected;

        public void Show()
        {
            _listView.SelectCell(0);
            _messageBus.SendAction(new SetGuiComponentVisibleAction{Widget = _window, IsVisble = true});
        }

        public void Close()
        {
            _messageBus.SendAction(new SetGuiComponentVisibleAction{Widget = _window, IsVisble = false});
        }

        protected virtual void OnItemSelected(Item i)
        {
            ItemSelected?.Invoke(this, new SelectionEventArgs<Item>(i));
        }

        protected void OnExitRequested()
        {
            ExitRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
