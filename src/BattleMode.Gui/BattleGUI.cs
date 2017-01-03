using System;
using System.Linq;
using Base;
using BattleMode.Entities.BattleState;
using BattleMode.Shared;
using GameEngine.Core;
using GameEngine.GUI;
using GameEngine.GUI.Components;
using GameEngine.TypeRegistry;

namespace BattleMode.Gui
{
    [GameService(typeof(IGUIService))]
    public class BattleGui : IGUIService
    {
        private readonly MessageBox _messageBox;

        public BattleGui(ScreenConstants screen, GuiManager manager,
            MessageBox messageBox, IEngineInterface engineInterface,
            MainMenuWidget mainWidget,
            MoveMenuWidget moveWidget, PokemonMenuWidget pokemonWidget,
            ItemMenuWidget itemWidget, IBattleStateService battleState,
            BattleData data) :
            this(screen, manager, messageBox, engineInterface, (IMenuWidget<MainMenuEntries>) mainWidget, moveWidget,
                pokemonWidget, itemWidget, battleState, data)
        {
        }

        internal BattleGui(ScreenConstants screen, GuiManager manager,
            MessageBox messageBox, IEngineInterface engineInterface,
            IMenuWidget<MainMenuEntries> mainWidget,
            IMenuWidget<Move> moveWidget, IMenuWidget<Pokemon> pokemonWidget,
            IMenuWidget<Item> itemWidget, IBattleStateService battleState,
            BattleData data)
        {
            _manager = manager;
            _battleState = battleState;
            _playerId = data.PlayerId;
            _ai = data.Clients.First(id => !id.IsPlayer);
            _moveWidget = moveWidget;
            _itemWidget = itemWidget;
            _mainWidget = mainWidget;
            _pokemonWidget = pokemonWidget;

            _messageBox = messageBox;

            InitMessageBox(screen);

            InitMainMenu(screen, engineInterface);
            InitAttackMenu(screen);
            InitItemMenu(screen);
            InitPkmnMenu(screen);

            manager.ShowWidget(_messageBox);
            manager.ShowWidget(_mainWidget);
        }

        public event EventHandler MenuShowed = delegate { };
        public event EventHandler TextDisplayed = delegate { };
        private readonly ClientIdentifier _ai;
        private readonly IMenuWidget<Move> _moveWidget;
        private readonly IMenuWidget<Item> _itemWidget;
        private readonly IMenuWidget<Pokemon> _pokemonWidget;

        private readonly GuiManager _manager;
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
            _manager.CloseWidget(_moveWidget);
        }

        private void BackToMain(object sender, EventArgs e)
        {
            _manager.CloseWidget(_itemWidget);
            _manager.CloseWidget(_moveWidget);
            _manager.CloseWidget(_pokemonWidget);

            _manager.ShowWidget(_mainWidget);
            _mainWidget.ResetSelection();
        }

        private void InitAttackMenu(ScreenConstants screen)
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
        }

        private void InitItemMenu(ScreenConstants screen)
        {
            var xPosition = 3.0f * screen.ScreenWidth / 8.0f;
            var yPosition = 1.0f * screen.ScreenHeight / 8.0f;

            var width = screen.ScreenWidth - xPosition;
            var height = (2.0f * screen.ScreenHeight / 3.0f) - yPosition;

            _itemWidget.SetCoordinates(xPosition, yPosition, width, height);

            _itemWidget.ItemSelected += ItemMenu_ItemSelected;
            _itemWidget.ExitRequested += BackToMain;
            _itemWidget.ExitRequested += delegate { _itemWidget.ResetSelection(); };
        }

        private void InitMainMenu(ScreenConstants screen, IEngineInterface engineInterface)
        {
            var xPosition = 0.5f * screen.ScreenWidth;
            var yPosition = 2.0f * screen.ScreenHeight / 3.0f;
            var width = screen.ScreenWidth - xPosition;
            var height = screen.ScreenHeight - yPosition;

            _mainWidget.SetCoordinates(xPosition, yPosition, width, height);
            _mainWidget.ItemSelected += MainMenu_ItemSelected;
            _mainWidget.ExitRequested += delegate { engineInterface.Exit(); };
        }

        private void InitMessageBox(ScreenConstants screen)
        {
            const int xPosition = 0;
            var yPosition = 2.0f * screen.ScreenHeight / 3.0f;
            var width = screen.ScreenWidth;
            var height = screen.ScreenHeight - yPosition;

            _messageBox.SetCoordinates(xPosition, yPosition, width, height);
            _messageBox.OnAllLineShowed += AllLineShowedHandler;
        }

        private void AllLineShowedHandler(object sender, EventArgs e)
        {
            TextDisplayed(this, EventArgs.Empty);
        }

        private void InitPkmnMenu(ScreenConstants screen)
        {
            const int xPosition = 0;
            const int yPosition = 0;
            var width = screen.ScreenWidth;
            var height = 2.0f * screen.ScreenHeight / 3.0f;

            _pokemonWidget.SetCoordinates(xPosition, yPosition, width, height);
            _pokemonWidget.ItemSelected += PKMNMenu_ItemSelected;
            _pokemonWidget.ExitRequested += BackToMain;
            _pokemonWidget.ExitRequested += delegate { _pokemonWidget.ResetSelection(); };
        }

        private void ItemMenu_ItemSelected(object sender, SelectionEventArgs<Item> e)
        {
            _battleState.SetItem(_playerId, _playerId, e.SelectedData);
            _manager.CloseWidget(_itemWidget);
        }

        private void MainMenu_ItemSelected(object sender, SelectionEventArgs<MainMenuEntries> e)
        {
            switch (e.SelectedData)
            {
                case MainMenuEntries.Attack:
                    _manager.CloseWidget(_mainWidget);
                    _manager.ShowWidget(_moveWidget);
                    _moveWidget.ResetSelection();
                    break;

                case MainMenuEntries.Pkmn:
                    _manager.CloseWidget(_mainWidget);
                    _manager.ShowWidget(_pokemonWidget);
                    _pokemonWidget.ResetSelection();
                    break;

                case MainMenuEntries.Item:
                    _manager.CloseWidget(_mainWidget);
                    _manager.ShowWidget(_itemWidget);
                    _itemWidget.ResetSelection();
                    break;
            }
        }

        private void PKMNMenu_ItemSelected(object sender, SelectionEventArgs<Pokemon> e)
        {
            _battleState.SetCharacter(_playerId, e.SelectedData);
            _manager.CloseWidget(_pokemonWidget);
        }

        private readonly IMenuWidget<MainMenuEntries> _mainWidget;
    }
}