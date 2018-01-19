using System;
using System.Collections.Generic;
using System.Linq;
using BattleMode.Shared;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Systems;
using GameEngine.GUI.Components;
using GameEngine.GUI.Loader;
using Microsoft.Xna.Framework;

namespace BattleMode.Gui
{
    public class GuiController
    {
        #pragma warning disable 649
        [GuiLoaderId("MessageBox")]
        private MessageBox _messageBox;
        #pragma warning restore 649

        private readonly Dictionary<ClientIdentifier, PokemonDataView> _dataViews = new Dictionary<ClientIdentifier, PokemonDataView>();

        public GuiController(GuiSystem manager,
            MainMenuController mainController,
            MoveMenuController moveController, PokemonMenuController pokemonController,
            ItemMenuController itemController, 
            PokemonDataView playerView, PokemonDataView aiView,
            IMessageBus messageBus,
            BattleData data)
        {
            manager.Factory.LoadFromFile(@"BattleMode\Gui\MessageBox.xml", this);
            var playerId = data.PlayerId;
            var ai = data.Clients.First(id => !id.IsPlayer);

            _dataViews[playerId] = playerView;
            _dataViews[ai] = aiView;

            _moveController = moveController;
            _itemController = itemController;
            _mainController = mainController;
            _pokemonController = pokemonController;

            InitMessageBox();

            _mainController.ItemSelected += MainMenu_ItemSelected;
            //_mainController.ExitRequested += delegate { engineInterface.Exit(); };

            //_itemController.ItemSelected += (obj, e) => battleState.SetItem(playerId, playerId, e.SelectedData);
            //_moveController.ItemSelected += (obj, e) => battleState.SetMove(playerId, ai, e.SelectedData);
            //_pokemonController.ItemSelected += (obj, e) => battleState.SetCharacter(playerId, e.SelectedData);

            OnItemSelectCloseAll();
            OnExitRequestedBackToMain();
            messageBus.SendAction(new SetGuiComponentVisibleAction(_messageBox, true));

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

        private void InitMessageBox()
        {
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