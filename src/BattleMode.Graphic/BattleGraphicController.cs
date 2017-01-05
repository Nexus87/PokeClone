using System;
using System.Collections.Generic;
using System.Linq;
using Base.Data;
using BattleMode.Entities.BattleState;
using BattleMode.Shared;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace BattleMode.Graphic
{
    [GameService(typeof(IBattleGraphicController))]
    public class BattleGraphicController : AbstractGuiComponent, IBattleGraphicController
    {
        private readonly Dictionary<ClientIdentifier, PokemonSprite> _sprites = new Dictionary<ClientIdentifier, PokemonSprite>();

        public BattleGraphicController(ScreenConstants screen, 

            PokemonSprite playerSprite, PokemonSprite aiSprite,
            BattleData data)
        {
            var player = data.PlayerId;
            var ai = data.Clients.First(id => !id.IsPlayer);

            playerSprite.IsPlayer = true;
            aiSprite.IsPlayer = false;



            _sprites[player] = playerSprite;
            _sprites[ai] = aiSprite;

            foreach (var sprite in _sprites.Values)
                sprite.OnPokemonAppeared += delegate { PokemonSet?.Invoke(this, null); };



            initAIGraphic(aiSprite, screen);
            initPlayerGraphic(playerSprite, screen);
        }

        public event EventHandler ConditionSet
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        public event EventHandler HpSet;
        public event EventHandler PokemonSet;

        public void PlayAttackAnimation(ClientIdentifier id)
        {
            _sprites[id].PlayAttackAnimation();
        }

        public void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon)
        {
            _sprites[id].SetPokemon(pokemon.ID);
        }

        public void SetPokemonStatus(ClientIdentifier id, StatusCondition condition)
        {
            throw new NotImplementedException();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            foreach (var sprite in _sprites.Values)
                sprite.Draw(time, batch);
        }

        private void initAIGraphic(PokemonSprite aiSprite, ScreenConstants screen)
        {
            var xPosition = screen.ScreenWidth * 0.6f;
            var yPosition = screen.ScreenHeight * 0.1f;

            var height = screen.ScreenHeight * 0.25f;
            var width = screen.ScreenHeight * 0.25f;

            aiSprite.SetCoordinates(xPosition, yPosition, width, height);
        }

        private void initPlayerGraphic(PokemonSprite playerSprite, ScreenConstants screen)
        {
            var xPosition = screen.ScreenWidth * 0.2f;
            var yPosition = screen.ScreenHeight * 0.4f;

            var height = screen.ScreenHeight * 0.25f;
            var width = screen.ScreenHeight * 0.25f;

            playerSprite.SetCoordinates(xPosition, yPosition, width, height);
        }
    }
}