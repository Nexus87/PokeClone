using System;
using System.Collections.ObjectModel;
using System.Linq;
using Base;
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
    public class ItemMenuWidget : AbstractGraphicComponent, IMenuWidget<Item>
    {
        private readonly Window _window;
        private readonly ListView<Item> _listView;

        public ItemMenuWidget(Window window, ScrollArea scrollArea, ListView<Item> listView, IGameTypeRegistry registry)
        {
            _window = window;
            _listView = listView;
            scrollArea.SetContent(listView);
            _window.SetContent(scrollArea);

            var model = Enumerable
                .Range(0, 20)
                .Select(x => new Item {Name = "Item" + x});

            _listView.Model = new ObservableCollection<Item>(model);
            _listView.ListCellFactory = value =>
            {
                var button = registry.ResolveType<Button>();
                button.Text = value.Name;
                return button;
            };
        }

        public event EventHandler ExitRequested;
        public event EventHandler<SelectionEventArgs<Item>> ItemSelected;

        public override void HandleKeyInput(CommandKeys key)
        {
            if (key == CommandKeys.Back)
                ExitRequested?.Invoke(this, EventArgs.Empty);
            else
                _window.HandleKeyInput(key);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _window.Draw(time, batch);
        }

        protected override void Update()
        {
            _window.SetCoordinates(this);
        }

        public void ResetSelection()
        {
            _listView.SelectCell(0);
        }
    }
}
