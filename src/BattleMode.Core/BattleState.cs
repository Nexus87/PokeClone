using System.Linq;
using BattleMode.Core.Entities;
using BattleMode.Entities.Actions;
using BattleMode.Entities.AI;
using BattleMode.Entities.Systems;
using BattleMode.Graphic;
using BattleMode.Gui;
using BattleMode.Gui.Actions;
using BattleMode.Shared;
using BattleMode.Shared.Actions;
using BattleMode.Shared.Components;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.GameStates;
using PokemonShared.Service;

namespace BattleMode.Core
{
    public class BattleState : State
    {
        private GuiControllerSystem _guiControllerSystem;

        public string ModuleName => "BattleMode";

        protected override void Init()
        {
            var spriteProvider = new SpriteProvider(TextureProvider);
            var playerEntity = PlayerEntity.Create(EntityManager, ScreenConstants);
            var aiEntity = AiEntity.Create(EntityManager, ScreenConstants);
            var battleStateEntity = BattleStateEntity.Create(EntityManager);
            var battleSystem = new BattleSystem(MessageBus, new DummyScheduler(), new DefaultMoveEffectCalculator(new DummyBattleRules()));
            _guiControllerSystem = new GuiControllerSystem(GuiSystem, MessageBus, playerEntity, aiEntity, spriteProvider);
            var graphicsSystem = new BattleGraphicController(spriteProvider);
            var aiSystem = new AiSystem(aiEntity, playerEntity, MessageBus);

            MessageBus.RegisterForAction<SetPlayerAction>(_guiControllerSystem.SetPlayer);
            MessageBus.RegisterForAction<SetPokemonAction>(_guiControllerSystem.SetPokemon);
            MessageBus.RegisterForAction<SetPokemonAction>(graphicsSystem.SetPokemon);

            MessageBus.RegisterForAction<ShowMainMenuAction>(_guiControllerSystem.ShowMainMenu);
            MessageBus.RegisterForAction<ShowMenuAction>(_guiControllerSystem.ShowMenu);

            MessageBus.RegisterForAction<SetCommandAction>(battleSystem.SetCommand);
            MessageBus.RegisterForAction<SetCommandAction>(_guiControllerSystem.SetCommand);
            MessageBus.RegisterForAction<ExecuteNextCommandAction>(battleSystem.ExecuteNextCommand);
            MessageBus.RegisterForAction<EndTurnAction>(battleSystem.EndTurn);
            MessageBus.RegisterForAction<UseMoveAction>(battleSystem.UseMove);
            MessageBus.RegisterForAction<UseMoveAction>(_guiControllerSystem.UseMove);

            MessageBus.RegisterForAction<ChangeHpAction>(_guiControllerSystem.ChangeHp);
            MessageBus.RegisterForAction<ShowMessageAction>(_guiControllerSystem.ShowMessage);
            MessageBus.RegisterForAction<DoDamageAction>(_guiControllerSystem.DoDamage);
            
            MessageBus.RegisterForAction<StartNewTurnAction>(aiSystem.StartNewTurn);
            var playerPokemon = EntityManager.GetComponentByTypeAndEntity<PokemonComponent>(playerEntity).First().Pokemon;
            var aiPokemon = EntityManager.GetComponentByTypeAndEntity<PokemonComponent>(playerEntity).First().Pokemon;
            
            MessageBus.SendAction(new StartNewTurnAction());
            MessageBus.SendAction(new SetGuiVisibleAction(true));
            MessageBus.SendAction(new SetPlayerAction(playerEntity));
            MessageBus.SendAction(new SetPokemonAction(playerEntity, playerPokemon));
            MessageBus.SendAction(new SetPokemonAction(aiEntity, aiPokemon));
        }

        public override void Pause()
        {
        }

        public override void Resume()
        {
        }

        public override void Stop()
        {
        }
    }
}