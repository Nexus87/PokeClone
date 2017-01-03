using System;
using System.Collections.Generic;
using System.Linq;
using Base.Data;
using BattleMode.Entities.BattleState;
using BattleMode.Gui;
using BattleMode.Shared;
using GameEngine.Core;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace BattleMode.Core.Components.GraphicComponents
{
    [GameService(typeof(IBattleGraphicController))]
    public class BattleGraphicController : AbstractGuiComponent, IBattleGraphicController
    {
        private readonly Dictionary<ClientIdentifier, PokemonDataView> _dataViews = new Dictionary<ClientIdentifier, PokemonDataView>();
        private readonly Dictionary<ClientIdentifier, PokemonSprite> _sprites = new Dictionary<ClientIdentifier, PokemonSprite>();

        public BattleGraphicController(ScreenConstants screen, 
            PlayerPokemonDataView playerView, AiPokemonDataView aiView, 
            PokemonSprite playerSprite, PokemonSprite aiSprite,
            BattleData data)
        {
            var player = data.PlayerId;
            var ai = data.Clients.First(id => !id.IsPlayer);

            playerSprite.IsPlayer = true;
            aiSprite.IsPlayer = false;

            _dataViews[player] = playerView;
            _dataViews[ai] = aiView;

            _sprites[player] = playerSprite;
            _sprites[ai] = aiSprite;

            foreach (var view in _dataViews.Values)
                view.HpUpdated += delegate { HpSet?.Invoke(this, null); };

            foreach (var sprite in _sprites.Values)
                sprite.OnPokemonAppeared += delegate { PokemonSet?.Invoke(this, null); };

            initAIGraphic(aiView, aiSprite, screen);
            initPlayerGraphic(playerView, playerSprite, screen);
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

        public void SetHp(ClientIdentifier id, int value)
        {
            _dataViews[id].SetHp(value);
        }

        public void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon)
        {
            _dataViews[id].SetPokemon(pokemon);
            _sprites[id].SetPokemon(pokemon.ID);
        }

        public void SetPokemonStatus(ClientIdentifier id, StatusCondition condition)
        {
            throw new NotImplementedException();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            foreach (var view in _dataViews.Values)
                view.Draw(time, batch);

            foreach (var sprite in _sprites.Values)
                sprite.Draw(time, batch);
        }

        private void initAIGraphic(PokemonDataView aiView, PokemonSprite aiSprite, ScreenConstants screen)
        {

            var xPosition = screen.ScreenWidth * 0.2f;
            var yPosition = screen.ScreenHeight * 0.1f;

            var height = screen.ScreenHeight * 0.1f;
            var width = screen.ScreenWidth * 0.15f;

            aiView.SetCoordinates(xPosition, yPosition, width, height);

            xPosition = screen.ScreenWidth * 0.6f;
            yPosition = screen.ScreenHeight * 0.1f;

            height = screen.ScreenHeight * 0.25f;
            width = screen.ScreenHeight * 0.25f;

            aiSprite.SetCoordinates(xPosition, yPosition, width, height);
        }

        private void initPlayerGraphic(PokemonDataView playerView, PokemonSprite playerSprite, ScreenConstants screen)
        {
            var xPosition = screen.ScreenWidth * 0.55f;
            var yPosition = screen.ScreenHeight * 0.45f;

            var height = screen.ScreenHeight * 0.15f;
            var width = screen.ScreenWidth * 0.15f;

            playerView.SetCoordinates(xPosition, yPosition, width, height);

            xPosition = screen.ScreenWidth * 0.2f;
            yPosition = screen.ScreenHeight * 0.4f;

            height = screen.ScreenHeight * 0.25f;
            width = screen.ScreenHeight * 0.25f;

            playerSprite.SetCoordinates(xPosition, yPosition, width, height);
        }
    }
}