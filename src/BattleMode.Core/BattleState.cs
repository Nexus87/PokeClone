using System.Linq;
using BattleMode.Core.Entities;
using BattleMode.Core.Systems;
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

        public string ModuleName => "BattleMode";

        protected override void Init()
        {
            var spriteProvider = new SpriteProvider(TextureProvider);
            var playerEntity = PlayerEntity.Create(EntityManager, ScreenConstants);
            var aiEntity = AiEntity.Create(EntityManager, ScreenConstants);
            var battleStateEntity = BattleStateEntity.Create(EntityManager);
            
            var battleSystem = new BattleSystem(new DummyScheduler(), new DefaultMoveEffectCalculator(new DummyBattleRules()));
            var guiControllerSystem = new GuiControllerSystem(GuiFactory, MessageBus, playerEntity, aiEntity, spriteProvider);
            var graphicsSystem = new BattleGraphicController(spriteProvider);
            var aiSystem = new AiSystem(aiEntity, playerEntity);

            GameDriverSystem.RegisterHandler(MessageBus);
            HpSystem.RegisterHandler(MessageBus);
            battleSystem.RegisterHandler(MessageBus);
            guiControllerSystem.RegisterHandler(MessageBus);
            graphicsSystem.RegisterHandler(MessageBus);
            aiSystem.RegisterHandler(MessageBus);

            var playerPokemon = EntityManager.GetComponentByTypeAndEntity<PokemonComponent>(playerEntity).First().Pokemon;
            var aiPokemon = EntityManager.GetComponentByTypeAndEntity<PokemonComponent>(playerEntity).First().Pokemon;
            
            MessageBus.SendStartNewTurnAction(this);
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