using Base.Data;
using BattleLib.Components.BattleState;
using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BattleLib.GraphicComponents
{
    public class BattleGraphicController : AbstractGraphicComponent, IBattleGraphicController
    {
        private readonly Dictionary<ClientIdentifier, PokemonDataView> dataViews = new Dictionary<ClientIdentifier, PokemonDataView>();
        private readonly Dictionary<ClientIdentifier, PokemonSprite> sprites = new Dictionary<ClientIdentifier, PokemonSprite>();

        public BattleGraphicController(
            ClientIdentifier playerId, ClientIdentifier aiId,
            PokemonDataView playerView, PokemonSprite playerSprite, 
            PokemonDataView aiView, PokemonSprite aiSprite)
        {
            dataViews[playerId] = playerView;
            dataViews[aiId] = aiView;

            sprites[playerId] = playerSprite;
            sprites[aiId] = aiSprite;

            foreach (var view in dataViews.Values)
                view.OnHPUpdated += delegate { OnHPSet(this, null); };

            foreach (var sprite in sprites.Values)
                sprite.OnPokemonAppeared += delegate { OnPokemonSet(this, null); };
        }

        public BattleGraphicController(IPokeEngine game, GraphicComponentFactory factory, ClientIdentifier player, ClientIdentifier ai)
        {
            var aiView = new PokemonDataView(game, factory, false);
            var playerView = new PokemonDataView(game, factory, true);
            var aiSprite = new PokemonSprite(true, game);
            var playerSprite = new PokemonSprite(false, game);

            dataViews[player] = playerView;
            dataViews[ai] = aiView;

            sprites[player] = playerSprite;
            sprites[ai] = aiSprite;

            foreach (var view in dataViews.Values)
                view.OnHPUpdated += delegate { OnHPSet(this, null); };

            foreach (var sprite in sprites.Values)
                sprite.OnPokemonAppeared += delegate { OnPokemonSet(this, null); };

            initAIGraphic(aiView, aiSprite, game);
            initPlayerGraphic(playerView, playerSprite, game);
        }

        public event EventHandler ConditionSet;
        public event EventHandler OnHPSet;
        public event EventHandler OnPokemonSet;

        public void PlayAttackAnimation(ClientIdentifier id)
        {
            sprites[id].PlayAttackAnimation();
        }

        public void SetHP(ClientIdentifier id, int value)
        {
            dataViews[id].SetHP(value);
        }

        public void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon)
        {
            dataViews[id].SetPokemon(pokemon);
            sprites[id].SetPokemon(pokemon.ID);
        }

        public void SetPokemonStatus(ClientIdentifier id, StatusCondition condition)
        {
            throw new NotImplementedException();
        }

        public override void Setup()
        {
            foreach (var view in dataViews.Values)
                view.Setup();

            foreach (var sprite in sprites.Values)
                sprite.Setup();

        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            foreach (var view in dataViews.Values)
                view.Draw(time, batch);

            foreach (var sprite in sprites.Values)
                sprite.Draw(time, batch);
        }

        private void initAIGraphic(PokemonDataView aiView, PokemonSprite aiSprite, IPokeEngine game)
        {
            aiView.XPosition = game.ScreenWidth * 0.2f;
            aiView.YPosition = game.ScreenHeight * 0.1f;

            aiView.Height = game.ScreenHeight * 0.25f;
            aiView.Width = game.ScreenWidth * 0.3f;

            aiSprite.XPosition = game.ScreenWidth * 0.6f;
            aiSprite.YPosition = game.ScreenHeight * 0.1f;

            aiSprite.Height = game.ScreenHeight * 0.25f;
            aiSprite.Width = game.ScreenHeight * 0.25f;
        }

        private void initPlayerGraphic(PokemonDataView playerView, PokemonSprite playerSprite, IPokeEngine game)
        {
            playerView.XPosition = game.ScreenWidth * 0.6f;
            playerView.YPosition = game.ScreenHeight * 0.4f;

            playerView.Height = game.ScreenHeight * 0.25f;
            playerView.Width = game.ScreenWidth * 0.3f;

            playerSprite.XPosition = game.ScreenWidth * 0.2f;
            playerSprite.YPosition = game.ScreenHeight * 0.4f;

            playerSprite.Height = game.ScreenHeight * 0.25f;
            playerSprite.Width = game.ScreenHeight * 0.25f;
        }
    }
}