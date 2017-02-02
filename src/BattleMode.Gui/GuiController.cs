using System;
using System.Collections.Generic;
using System.Linq;
using BattleMode.Entities.BattleState;
using BattleMode.Shared;
using GameEngine.Core;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Components;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace BattleMode.Gui
{
    [GameService(typeof(IGuiController))]
    public class GuiController : IGuiController
    {
        private readonly MessageBox _messageBox;
        private readonly Dictionary<ClientIdentifier, IPokemonDataView> _dataViews = new Dictionary<ClientIdentifier, IPokemonDataView>();

        public GuiController(ScreenConstants screen, GuiManager manager,
            MessageBox messageBox, IEngineInterface engineInterface,
            MainMenuController mainController,
            MoveMenuController moveController, PokemonMenuController pokemonController,
            ItemMenuController itemController, IBattleStateService battleState,
            PlayerPokemonDataView playerView, AiPokemonDataView aiView,
            BattleData data)
        {
            var playerId = data.PlayerId;
            var ai = data.Clients.First(id => !id.IsPlayer);

            _dataViews[playerId] = playerView;
            _dataViews[ai] = aiView;

            _moveController = moveController;
            _itemController = itemController;
            _mainController = mainController;
            _pokemonController = pokemonController;

            _messageBox = messageBox;

            InitMessageBox(screen);

            _mainController.ItemSelected += MainMenu_ItemSelected;
            _mainController.ExitRequested += delegate { engineInterface.Exit(); };

            _itemController.ItemSelected += (obj, e) => battleState.SetItem(playerId, playerId, e.SelectedData);
            _moveController.ItemSelected += (obj, e) => battleState.SetMove(playerId, ai, e.SelectedData);
            _pokemonController.ItemSelected += (obj, e) => battleState.SetCharacter(playerId, e.SelectedData);

            OnItemSelectCloseAll();
            OnExitRequestedBackToMain();
            manager.ShowWidget(_messageBox);

            _mainController.Show();

            foreach(var view in _dataViews.Values)
                view.Show();
        }

        private void OnExitRequestedBackToMain()
        {
            _itemController.ExitRequested += delegate { BackToMain(); };
            _moveController.ExitRequested += delegate { BackToMain(); };
            _pokemonController.ExitRequested += delegate { BackToMain(); };
        }

        private void OnItemSelectCloseAll()
        {
            _itemController.ItemSelected += delegate { CloseAll(); };
            _moveController.ItemSelected += delegate { CloseAll(); };
            _pokemonController.ItemSelected += delegate { CloseAll(); };
        }

        private void CloseAll()
        {
            _mainController.Close();
            _itemController.Close();
            _moveController.Close();
            _pokemonController.Close();
        }

        public event EventHandler MenuShowed = delegate { };
        public event EventHandler TextDisplayed = delegate { };
        private readonly MoveMenuController _moveController;
        private readonly ItemMenuController _itemController;
        private readonly PokemonMenuController _pokemonController;

        public void SetText(string text)
        {
            _messageBox.DisplayText(text);
        }

        public void ShowMenu()
        {
            _messageBox.ResetText();
            BackToMain();
            MenuShowed(this, EventArgs.Empty);
        }

        private void BackToMain()
        {
            _itemController.Close();
            _moveController.Close();
            _pokemonController.Close();

            _mainController.Show();
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


        private void MainMenu_ItemSelected(object sender, SelectionEventArgs<MainMenuEntries> e)
        {
            if (e.SelectedData == MainMenuEntries.Run)
                return;

            _mainController.Close();

            switch (e.SelectedData)
            {
                case MainMenuEntries.Attack:
                    _moveController.Show();
                    break;
                case MainMenuEntries.Pkmn:
                    _pokemonController.Show();
                    break;
                case MainMenuEntries.Item:
                    _itemController.Show();
                    break;
            }
        }


        private readonly MainMenuController _mainController;
        private Action _action;

        public void Update(GameTime time)
        {
            _action?.Invoke();
        }

        public void SetHp(ClientIdentifier target, int hp)
        {
            var dataView = _dataViews[target];
            if (hp > dataView.CurrentHp)
            {
                _action = () =>
                {
                    dataView.SetHp(dataView.CurrentHp + 1);
                    if (dataView.CurrentHp != hp)
                        return;
                    _action = null;
                    HpSet?.Invoke(this, EventArgs.Empty);
                };
            }
            else
            {
                _action = () =>
                {
                    dataView.SetHp(dataView.CurrentHp - 1);
                    if (dataView.CurrentHp != hp)
                        return;
                    _action = null;
                    HpSet?.Invoke(this, EventArgs.Empty);
                };
            }
        }

        public void SetPokemon(ClientIdentifier id, PokemonEntity pokemon)
        {
            _dataViews[id].SetPokemon(pokemon);
        }

        public event EventHandler HpSet;
    }
}