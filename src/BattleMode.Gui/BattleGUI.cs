using System;
using System.Linq;
using Base;
using BattleMode.Components.BattleState;
using BattleMode.Shared;
using GameEngine.Core;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.GUI;
using GameEngine.GUI.Panels;
using GameEngine.TypeRegistry;

namespace BattleMode.Gui
{
    [GameService(typeof(IGUIService))]
    public class BattleGui : IGUIService
    {
        private readonly MessageBox _messageBox;
        private readonly Window _messageWindow;

        public BattleGui(ScreenConstants screen, GuiManager manager, 
            Window messageWindow, MessageBox messageBox, IEngineInterface engineInterface,
            MainMenuWidget mainWidget,
            MoveMenuWidget moveWidget, PokemonMenuWidget pokemonWidget,
            ItemMenuWidget itemWidget, IBattleStateService battleState,
            BattleData data) :
            this(screen, manager, messageWindow, messageBox, engineInterface, (IMenuWidget<MainMenuEntries>)mainWidget, moveWidget, pokemonWidget, itemWidget, battleState, data)
        {}
        
            internal BattleGui(ScreenConstants screen, GuiManager manager, 
            Window messageWindow, MessageBox messageBox, IEngineInterface engineInterface,
            IMenuWidget<MainMenuEntries> mainWidget,
            IMenuWidget<Move> moveWidget, IMenuWidget<Pokemon> pokemonWidget, 
            IMenuWidget<Item> itemWidget, IBattleStateService battleState, 
            BattleData data)
        {
            _battleState = battleState;
            _playerId = data.PlayerId;
            _ai = data.Clients.First(id => !id.IsPlayer);
            _moveWidget = moveWidget;
            _itemWidget = itemWidget;
            MainWidget = mainWidget;
            _pokemonWidget = pokemonWidget;

            _messageBox = messageBox;
            _messageWindow = messageWindow;

            InitMessageBox(screen, manager);

            InitMainMenu(screen, manager, engineInterface);
            InitAttackMenu(screen, manager);
            InitItemMenu(screen, manager);
            InitPkmnMenu(screen, manager);

        }

        public event EventHandler MenuShowed = delegate { };
        public event EventHandler TextDisplayed = delegate { };
        private readonly ClientIdentifier _ai;
        private readonly IMenuWidget<Move> _moveWidget;
        private readonly IMenuWidget<Item> _itemWidget;
        private readonly IMenuWidget<Pokemon> _pokemonWidget;

        private readonly IBattleStateService _battleState;
        private readonly ClientIdentifier _playerId;

        public void SetText(string text)
        {
            _messageBox.DisplayText(text);
        }

        public void ShowMenu()
        {
            _messageBox.ResetText();
            BackToMain(null, null);
            MenuShowed(this, EventArgs.Empty);
        }

        private void AttackMenu_ItemSelected(object sender, SelectionEventArgs<Move> e)
        {
            _battleState.SetMove(_playerId, _ai, e.SelectedData);
            _moveWidget.IsVisible = false;
        }

        private void BackToMain(object sender, EventArgs e)
        {
            _itemWidget.IsVisible = false;
            _moveWidget.IsVisible = false;
            _pokemonWidget.IsVisible = false;

            MainWidget.IsVisible = true;
        }

        private void InitAttackMenu(ScreenConstants screen, GuiManager manager)
        {
            _moveWidget.SetCoordinates(
                screen.ScreenWidth / 2.0f,
                2.0f * screen.ScreenHeight / 3.0f,
                width: screen.ScreenWidth - screen.ScreenWidth / 2.0f,
                height: screen.ScreenHeight - 2.0f * screen.ScreenHeight / 3.0f
            );

            _moveWidget.ItemSelected += AttackMenu_ItemSelected;
            _moveWidget.ExitRequested += BackToMain;
            _moveWidget.ExitRequested += delegate { _moveWidget.ResetSelection(); };

            _moveWidget.IsVisible = false;
            manager.AddWidget(_moveWidget);
        }

        private void InitItemMenu(ScreenConstants screen, GuiManager manager)
        {
            var xPosition = 3.0f * screen.ScreenWidth / 8.0f;
            var yPosition = 1.0f * screen.ScreenHeight / 8.0f;

            var width = screen.ScreenWidth - xPosition;
            var height = (2.0f * screen.ScreenHeight / 3.0f) - yPosition;

            _itemWidget.SetCoordinates(xPosition, yPosition, width, height);

            _itemWidget.ItemSelected += ItemMenu_ItemSelected;
            _itemWidget.ExitRequested += BackToMain;
            _itemWidget.ExitRequested += delegate { _itemWidget.ResetSelection(); };

            _itemWidget.IsVisible = false;

            manager.AddWidget(_itemWidget);
        }

        private void InitMainMenu(ScreenConstants screen, GuiManager manager, IEngineInterface engineInterface)
        {
            var xPosition = 0.5f * screen.ScreenWidth;
            var yPosition = 2.0f * screen.ScreenHeight / 3.0f;
            var width = screen.ScreenWidth - xPosition;
            var height = screen.ScreenHeight - yPosition;

            MainWidget.SetCoordinates(xPosition, yPosition, width, height);
            MainWidget.ItemSelected += MainMenu_ItemSelected;
            MainWidget.ExitRequested += delegate { engineInterface.Exit(); };

            MainWidget.IsVisible = true;
            manager.AddWidget(MainWidget);
        }

        private void InitMessageBox(ScreenConstants screen, GuiManager manager)
        {
            _messageWindow.SetContent(_messageBox);
            const int xPosition = 0;
            var yPosition = 2.0f * screen.ScreenHeight / 3.0f;
            var width = screen.ScreenWidth;
            var height = screen.ScreenHeight - yPosition;

            _messageWindow.SetCoordinates(xPosition, yPosition, width, height);
            _messageWindow.IsVisible = true;
            manager.AddWidget(_messageWindow);

            _messageBox.OnAllLineShowed += AllLineShowedHandler;
        }

        private void AllLineShowedHandler(object sender, EventArgs e)
        {
            TextDisplayed(this, EventArgs.Empty);
        }

        private void InitPkmnMenu(ScreenConstants screen, GuiManager manager)
        {
            const int xPosition = 0;
            const int yPosition = 0;
            var width = screen.ScreenWidth;
            var height = 2.0f * screen.ScreenHeight / 3.0f;

            _pokemonWidget.SetCoordinates(xPosition, yPosition, width, height);
            _pokemonWidget.ItemSelected += PKMNMenu_ItemSelected;
            _pokemonWidget.ExitRequested += BackToMain;
            _pokemonWidget.ExitRequested += delegate { _pokemonWidget.ResetSelection(); };

            _pokemonWidget.IsVisible = false;

            manager.AddWidget(_pokemonWidget);
        }

        private void ItemMenu_ItemSelected(object sender, SelectionEventArgs<Item> e)
        {
            _battleState.SetItem(_playerId, _playerId, e.SelectedData);
            _itemWidget.IsVisible = false;
        }

        private void MainMenu_ItemSelected(object sender, SelectionEventArgs<MainMenuEntries> e)
        {
            switch (e.SelectedData)
            {
                case MainMenuEntries.Attack:
                    MainWidget.IsVisible = false;
                    _moveWidget.IsVisible = true;
                    break;

                case MainMenuEntries.Pkmn:
                    MainWidget.IsVisible = false;
                    _pokemonWidget.IsVisible = true;
                    break;

                case MainMenuEntries.Item:
                    MainWidget.IsVisible = false;
                    _itemWidget.IsVisible = true;
                    break;
            }
        }

        private void PKMNMenu_ItemSelected(object sender, SelectionEventArgs<Pokemon> e)
        {
            _battleState.SetCharacter(_playerId, e.SelectedData);
            _pokemonWidget.IsVisible = false;
        }

        public IMenuWidget<MainMenuEntries> MainWidget { get; set; }
    }
}