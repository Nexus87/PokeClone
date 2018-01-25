using System;
using System.Collections.Generic;
using System.Linq;
using BattleMode.Entities.Actions;
using BattleMode.Gui.Actions;
using BattleMode.Shared;
using BattleMode.Shared.Actions;
using BattleMode.Shared.Components;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Systems;
using GameEngine.GUI;
using GameEngine.GUI.Components;
using GameEngine.GUI.Loader;
using PokemonShared.Service;

namespace BattleMode.Gui
{
    public class GuiControllerSystem
    {
        private readonly Dictionary<Guid, PokemonDataView> _dataViews = new Dictionary<Guid, PokemonDataView>();
        private readonly ItemMenuController _itemController;


        private readonly MainMenuController _mainController;
        private readonly MoveMenuController _moveController;
        private readonly PokemonMenuController _pokemonController;

        private readonly IMessageBus _messageBus;
#pragma warning disable 649
        [GuiLoaderId("MessageBox")] private MessageBox _messageBox;
        private readonly Guid _playerId;
#pragma warning restore 649

        public GuiControllerSystem(GuiFactory guiFactory, IMessageBus messageBus, Entity player, Entity ai,
            SpriteProvider spriteProvider) :
            this(guiFactory,
                new MainMenuController(guiFactory, messageBus),
                new MoveMenuController(guiFactory, messageBus, player, ai),
                new PokemonMenuController(guiFactory, messageBus, spriteProvider),
                new ItemMenuController(guiFactory, messageBus),
                new PokemonDataView(guiFactory, @"BattleMode\Gui\PlayerDataView.xml", messageBus, player),
                new PokemonDataView(guiFactory, @"BattleMode\Gui\AiDataView.xml", messageBus, ai),
                messageBus,
                player, ai
            )
        {
        }

        public GuiControllerSystem(GuiFactory guiFactory,
            MainMenuController mainController,
            MoveMenuController moveController, PokemonMenuController pokemonController,
            ItemMenuController itemController,
            PokemonDataView playerView, PokemonDataView aiView,
            IMessageBus messageBus,
            Entity player, Entity ai
        )
        {
            guiFactory.LoadFromFile(@"BattleMode\Gui\MessageBox.xml", this);
            _playerId = player.Id;
            var aiId = ai.Id;

            _dataViews[_playerId] = playerView;
            _dataViews[aiId] = aiView;

            _moveController = moveController;
            _itemController = itemController;
            _mainController = mainController;
            _pokemonController = pokemonController;
            _messageBus = messageBus;
            _messageBox.OnAllLineShowed += delegate
            {
                _messageBus.SendAction(new ShowMessageAction(""));
                _messageBus.SendAction(new UnblockQueueAction());
            };

            messageBus.SendAction(new SetGuiComponentVisibleAction(_messageBox, true));

            _mainController.Show();

            foreach (var view in _dataViews.Values)
                view.Show();
        }
        public void Update(IEntityManager entityManager, IMessageBus messageBus) 
        {
            foreach (var view in _dataViews.Values)
            {
                view.Update(entityManager);
            }
        }
        public void SetCommand(SetCommandAction action, IMessageBus messageBus)
        {
            if (action.Entity.Id == _playerId)
            {
                CloseAll(messageBus);
            }
        }

        public void ShowMenu(ShowMenuAction action, IMessageBus messageBus)
        {
            if (action.Menu == MainMenuEntries.Run)
                return;

            _mainController.Close();

            switch (action.Menu)
            {
                case MainMenuEntries.Attack:
                    _moveController.Show(messageBus);
                    break;
                case MainMenuEntries.Pkmn:
                    _pokemonController.Show(messageBus);
                    break;
                case MainMenuEntries.Item:
                    _itemController.Show(messageBus);
                    break;
            }
        }

        public void ShowMainMenu(IMessageBus messageBus)
        {
            CloseAll(messageBus);
            _mainController.Show();
        }
        private void CloseAll(IMessageBus messageBus)
        {
            _mainController.Close();
            _itemController.Close(messageBus);
            _moveController.Close(messageBus);
            _pokemonController.Close(messageBus);
        }

        public void SetPokemon(SetPokemonAction action, IMessageBus messageBus)
        {
            if (_dataViews.TryGetValue(action.Entity.Id, out var value))
            {
                value.SetPokemon(action.Pokemon);
            }
            if (action.Entity.Id == _playerId)
            {
                _moveController.SetPokemon(action.Pokemon);
            }
        }

        public void SetPlayer(SetPlayerAction action, IEntityManager entityManager, IMessageBus messageBus)
        {
            var trainerComponent = entityManager.GetComponentByTypeAndEntity<TrainerComponent>(action.PlayerEntity).First();
            _pokemonController.SetPlayerPokemon(trainerComponent.Pokemons);
            _itemController.SetItems(trainerComponent.Items);
        }

        public void ShowMessage(ShowMessageAction action, IMessageBus messageBus)
        {
            _messageBox.DisplayText(action.Text);
        }

    }
}