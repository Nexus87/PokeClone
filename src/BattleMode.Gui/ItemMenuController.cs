﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using GameEngine.Core;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Loader;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;
using PokemonShared.Models;

namespace BattleMode.Gui
{
    [GameType]
    public class ItemMenuController
    {
        private readonly GuiManager _guiManager;
#pragma warning disable 649

        [GuiLoaderId("Window")]
        private Window _window;
        [GuiLoaderId("ListView")]
        private ListView<Item> _listView;

#pragma warning restore 649

        public ItemMenuController(GuiManager guiManager, IGameTypeRegistry registry)
        {
            _guiManager = guiManager;

            var loader = new GuiLoader(@"BattleMode\Gui\ItemMenu.xml") {Controller = this};
            loader.Load();

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

            _window.SetInputListener(CommandKeys.Back, OnExitRequested);

        }

        public event EventHandler ExitRequested;
        public event EventHandler<SelectionEventArgs<Item>> ItemSelected;

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

        protected void OnExitRequested()
        {
            ExitRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
