using System;
using System.Collections.Generic;
using System.Linq;
using BattleMode.Shared.Actions;
using BattleMode.Shared.Components;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Components;
using GameEngine.Globals;
using Microsoft.Xna.Framework;
using PokemonShared.Service;

namespace BattleMode.Graphic
{
    public class BattleGraphicController
    {
        private readonly SpriteProvider _provider;

        public BattleGraphicController(SpriteProvider provider)
        {
            _provider = provider;
        }

        public void SetPokemon(SetPokemonAction action, IEntityManager entityManager, IMessageBus messageBus)
        {
            var renderComponent = entityManager.GetComponentByTypeAndEntity<RenderComponent>(action.Entity).FirstOrDefault();
            renderComponent.Texture = entityManager.HasComponent<PlayerComponent>(action.Entity)
                ? _provider.GetTextureBack(action.Pokemon.Id)
                : _provider.GetTexturesFront(action.Pokemon.Id);
        }



        public static Rectangle InitAIGraphic(ScreenConstants screen)
        {

            var xPosition = screen.ScreenWidth * 0.6f;
            var yPosition = screen.ScreenHeight * 0.1f;

            var height = screen.ScreenHeight * 0.25f;
            var width = screen.ScreenHeight * 0.25f;

            return new Rectangle((int)xPosition, (int)yPosition, (int)width, (int)height);
        }

        public static Rectangle InitPlayerGraphic(ScreenConstants screen)
        {
            
            var xPosition = screen.ScreenWidth * 0.2f;
            var yPosition = screen.ScreenHeight * 0.4f;

            var height = screen.ScreenHeight * 0.25f;
            var width = screen.ScreenHeight * 0.25f;

            return new Rectangle((int)xPosition, (int)yPosition, (int)width, (int)height);
        }
    }
}