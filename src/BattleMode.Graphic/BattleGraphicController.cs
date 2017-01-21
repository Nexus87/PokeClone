using System;
using System.Collections.Generic;
using System.Linq;
using Base.Data;
using Base.Rules;
using BattleMode.Entities.BattleState;
using BattleMode.Shared;
using GameEngine.Core;
using GameEngine.Globals;
using GameEngine.Graphics;
using GameEngine.GUI;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace BattleMode.Graphic
{
    [GameService(typeof(IBattleGraphicController))]
    public class BattleGraphicController : AbstractGuiComponent, IBattleGraphicController
    {
        private readonly SpriteProvider _provider;
        private readonly Dictionary<ClientIdentifier, Sprite> _sprites = new Dictionary<ClientIdentifier, Sprite>();

        public BattleGraphicController(ScreenConstants screen, SpriteProvider provider, BattleData data)
        {
            _provider = provider;
            var player = data.PlayerId;
            var ai = data.Clients.First(id => !id.IsPlayer);

            _sprites[player] = new Sprite();
            _sprites[ai] = new Sprite();

            initAIGraphic(_sprites[ai], screen);
            initPlayerGraphic(_sprites[player], screen);

            Scene = new Scene(screen);
            foreach (var spritesValue in _sprites.Values)
            {
                Scene.AddSprite(spritesValue);
            }
        }

        public void PlayAttackAnimation(ClientIdentifier id)
        {
        }

        public void SetPokemon(ClientIdentifier id, PokemonEntity pokemon)
        {
            _sprites[id].Texture = id.IsPlayer
                ? _provider.GetTextureBack(pokemon.Id)
                : _provider.GetTexturesFront(pokemon.Id);
        }

        public void SetPokemonStatus(ClientIdentifier id, StatusCondition condition)
        {
            throw new NotImplementedException();
        }

        public Scene Scene { get; }


        private void initAIGraphic(Sprite aiSprite, ScreenConstants screen)
        {
            var xPosition = screen.ScreenWidth * 0.6f;
            var yPosition = screen.ScreenHeight * 0.1f;

            var height = screen.ScreenHeight * 0.25f;
            var width = screen.ScreenHeight * 0.25f;

            aiSprite.Position = new Rectangle((int) xPosition, (int) yPosition, (int) width, (int) height);
        }

        private void initPlayerGraphic(Sprite playerSprite, ScreenConstants screen)
        {
            var xPosition = screen.ScreenWidth * 0.2f;
            var yPosition = screen.ScreenHeight * 0.4f;

            var height = screen.ScreenHeight * 0.25f;
            var width = screen.ScreenHeight * 0.25f;

            playerSprite.Position = new Rectangle((int) xPosition, (int) yPosition, (int) width, (int) height);
        }
    }
}