using System;
using System.Collections.Generic;
using System.Linq;
using BattleMode.Shared;
using BattleMode.Shared.Actions;
using BattleMode.Shared.Components;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Systems;
using GameEngine.GUI.Components;
using GameEngine.GUI.Loader;

namespace BattleMode.Gui
{
    public class GuiControllerSystem
    {
        private readonly Dictionary<Guid, PokemonDataView> _dataViews = new Dictionary<Guid, PokemonDataView>();
        private readonly ItemMenuController _itemController;


        private readonly MainMenuController _mainController;
        private readonly MoveMenuController _moveController;
        private readonly PokemonMenuController _pokemonController;
#pragma warning disable 649
        [GuiLoaderId("MessageBox")] private MessageBox _messageBox;
#pragma warning restore 649

        public GuiControllerSystem(GuiSystem system, IMessageBus messageBus, Entity player, Entity ai) :
            this(system,
                new MainMenuController(system.Factory, messageBus),
                new MoveMenuController(new BattleData(), system.Factory, messageBus),
                new PokemonMenuController(system.Factory, messageBus),
                new ItemMenuController(system.Factory, messageBus),
                new PokemonDataView(system.Factory, @"BattleMode\Gui\PlayerDataView.xml", messageBus),
                new PokemonDataView(system.Factory, @"BattleMode\Gui\AiDataView.xml", messageBus),
                messageBus,
                player, ai
            )
        {
        }

        public GuiControllerSystem(GuiSystem manager,
            MainMenuController mainController,
            MoveMenuController moveController, PokemonMenuController pokemonController,
            ItemMenuController itemController,
            PokemonDataView playerView, PokemonDataView aiView,
            IMessageBus messageBus,
            Entity player, Entity ai
        )
        {
            manager.Factory.LoadFromFile(@"BattleMode\Gui\MessageBox.xml", this);
            var playerId = player.Id;
            var aiId = ai.Id;

            _dataViews[playerId] = playerView;
            _dataViews[aiId] = aiView;

            _moveController = moveController;
            _itemController = itemController;
            _mainController = mainController;
            _pokemonController = pokemonController;

            _mainController.ItemSelected += MainMenu_ItemSelected;

            OnItemSelectCloseAll();
            OnExitRequestedBackToMain();
            messageBus.SendAction(new SetGuiComponentVisibleAction(_messageBox, true));

            _mainController.Show();

            foreach (var view in _dataViews.Values)
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

        public void ShowMenu()
        {
            _messageBox.ResetText();
            BackToMain();
        }

        private void BackToMain()
        {
            _itemController.Close();
            _moveController.Close();
            _pokemonController.Close();

            _mainController.Show();
        }

        public void SetPokemon(SetPokemonAction action, IEntityManager entityManager)
        {
            if (_dataViews.TryGetValue(action.Entity.Id, out var value))
            {
                value.SetPokemon(action.Pokemon);
            }
        }

        public void SetPlayer(SetPlayerAction action, IEntityManager entityManager)
        {
            var trainerComponent = entityManager.GetComponentByTypeAndEntity<TrainerComponent>(action.PlayerEntity).First();
            _pokemonController.SetPlayerPokemon(trainerComponent.Pokemons);
            _itemController.SetItems(trainerComponent.Items);
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
    }
}