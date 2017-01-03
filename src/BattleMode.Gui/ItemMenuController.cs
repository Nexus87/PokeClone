using System;
using System.Collections.ObjectModel;
using System.Linq;
using Base;
using GameEngine.Core;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;

namespace BattleMode.Gui
{
    [GameType]
    public class ItemMenuController
    {
        private readonly GuiManager _guiManager;
        private readonly Window _window;
        private readonly ListView<Item> _listView;

        public ItemMenuController(GuiManager guiManager, ScreenConstants screenConstants, Window window, ScrollArea scrollArea, ListView<Item> listView, IGameTypeRegistry registry)
        {
            _guiManager = guiManager;
            _window = window;
            _listView = listView;
            scrollArea.Content = listView;
            _window.SetContent(scrollArea);

            var model = Enumerable
                .Range(0, 20)
                .Select(x => new Item {Name = "Item" + x});

            _listView.Model = new ObservableCollection<Item>(model);
            _listView.ListCellFactory = value =>
            {
                var button = registry.ResolveType<Button>();
                button.Text = value.Name;
                button.ButtonPressed += delegate { OnItemSelected(value); };
                return button;
            };

            InitWindow(screenConstants);
        }

        public event EventHandler ExitRequested;
        public event EventHandler<SelectionEventArgs<Item>> ItemSelected;

        public void HandleKeyInput(CommandKeys key)
        {
            if (key == CommandKeys.Back)
                ExitRequested?.Invoke(this, EventArgs.Empty);
            else
                _window.HandleKeyInput(key);
        }

        public void Show()
        {
            _listView.SelectCell(0);
            _guiManager.ShowWidget(_window);
        }

        public void Close()
        {
            _guiManager.CloseWidget(_window);
        }

        protected virtual void OnItemSelected(Item i)
        {
            ItemSelected?.Invoke(this, new SelectionEventArgs<Item>(i));
        }

        private void InitWindow(ScreenConstants screen)
        {
            var xPosition = 3.0f * screen.ScreenWidth / 8.0f;
            var yPosition = 1.0f * screen.ScreenHeight / 8.0f;

            var width = screen.ScreenWidth - xPosition;
            var height = (2.0f * screen.ScreenHeight / 3.0f) - yPosition;

            _window.SetCoordinates(xPosition, yPosition, width, height);
            _window.SetInputListener(CommandKeys.Back, OnExitRequested);
        }

        protected void OnExitRequested()
        {
            ExitRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
