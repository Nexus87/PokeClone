﻿using System.Linq;
using BattleMode.Core.Entities;
using BattleMode.Graphic;
using BattleMode.Gui;
using BattleMode.Gui.Actions;
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
            _guiControllerSystem = new GuiControllerSystem(GuiSystem, MessageBus, playerEntity, aiEntity, spriteProvider);
            var graphicsSystem = new BattleGraphicController(spriteProvider);

            MessageBus.RegisterForAction<SetPlayerAction>(_guiControllerSystem.SetPlayer);
            MessageBus.RegisterForAction<SetPokemonAction>(_guiControllerSystem.SetPokemon);
            MessageBus.RegisterForAction<SetPokemonAction>(graphicsSystem.SetPokemon);

            MessageBus.RegisterForAction<ShowMainMenuAction>(_guiControllerSystem.ShowMainMenu);
            MessageBus.RegisterForAction<ShowMenuAction>(_guiControllerSystem.ShowMenu);

            var playerPokemon = EntityManager.GetComponentByTypeAndEntity<PokemonComponent>(playerEntity).First().Pokemon;
            var aiPokemon = EntityManager.GetComponentByTypeAndEntity<PokemonComponent>(playerEntity).First().Pokemon;
            
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