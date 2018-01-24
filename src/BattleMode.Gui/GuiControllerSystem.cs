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

        public GuiControllerSystem(GuiSystem system, IMessageBus messageBus, Entity player, Entity ai,
            SpriteProvider spriteProvider) :
            this(system,
                new MainMenuController(system.Factory, messageBus),
                new MoveMenuController(system.Factory, messageBus, player, ai),
                new PokemonMenuController(system.Factory, messageBus, spriteProvider),
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

        public void SetCommand(SetCommandAction action)
        {
            if (action.Entity.Id == _playerId)
            {
                CloseAll();
            }
        }

        public void ShowMenu(ShowMenuAction action, IEntityManager entityManager)
        {
            if (action.Menu == MainMenuEntries.Run)
                return;

            _mainController.Close();

            switch (action.Menu)
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

        public void ShowMainMenu(ShowMainMenuAction action, IEntityManager entityManager)
        {
            CloseAll();
            _mainController.Show();
        }
        private void CloseAll()
        {
            _mainController.Close();
            _itemController.Close();
            _moveController.Close();
            _pokemonController.Close();
        }

        public void SetPokemon(SetPokemonAction action, IEntityManager entityManager)
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

        public void SetPlayer(SetPlayerAction action, IEntityManager entityManager)
        {
            var trainerComponent = entityManager.GetComponentByTypeAndEntity<TrainerComponent>(action.PlayerEntity).First();
            _pokemonController.SetPlayerPokemon(trainerComponent.Pokemons);
            _itemController.SetItems(trainerComponent.Items);
        }

        public void ShowMessage(ShowMessageAction action)
        {
            _messageBox.DisplayText(action.Text);
        }

        public void ChangeHp(ChangeHpAction action)
        {
            var view = _dataViews[action.Target.Id];
            view.SetHp(view.CurrentHp + action.Diff);
            _messageBus.SendAction(new UnblockQueueAction());
        }
        public void UseMove(UseMoveAction action, IEntityManager entityManager)
        {
            var pokemon = entityManager.GetComponentByTypeAndEntity<PokemonComponent>(action.Source).First();
            _messageBus.SendAction(new QueueAction(new ShowMessageAction($"{pokemon.Pokemon.Name} uses {action.Move.Name}")));
        }

        public void DoDamage(DoDamageAction action, IEntityManager entityManager)
        {
            _messageBus.SendAction(new QueueAction(new ChangeHpAction(-action.Damage, action.Target)));
            if (action.Critical)
            {
                _messageBus.SendAction(new QueueAction(new ShowMessageAction("Critical!")));
            }

            if (action.MoveEfficiency == MoveEfficiency.NoEffect)
            {
                _messageBus.SendAction(new QueueAction(new ShowMessageAction("It has no effect!")));

            }
            else if (action.MoveEfficiency == MoveEfficiency.NotEffective)
            {
                _messageBus.SendAction(new QueueAction(new ShowMessageAction("It is not very effective!")));
            }
            else if (action.MoveEfficiency == MoveEfficiency.VeryEffective)
            {
                _messageBus.SendAction(new QueueAction(new ShowMessageAction("It is very effective!")));
            }
        }
    }
}