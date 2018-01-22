using BattleMode.Core.Entities;
using BattleMode.Gui;
using BattleMode.Shared.Actions;
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
            var playerEntity = PlayerEntity.Create(EntityManager);
            var aiEntity = AiEntity.Create(EntityManager);
            _guiControllerSystem = new GuiControllerSystem(GuiSystem, MessageBus, playerEntity, aiEntity, spriteProvider);


            MessageBus.RegisterForAction<SetPlayerAction>(_guiControllerSystem.SetPlayer);
            MessageBus.RegisterForAction<SetPokemonAction>(_guiControllerSystem.SetPokemon);


            MessageBus.SendAction(new SetGuiVisibleAction(true));
            MessageBus.SendAction(new SetPlayerAction(playerEntity));
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