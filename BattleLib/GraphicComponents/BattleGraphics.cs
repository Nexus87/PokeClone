using Base;
using Base.Data;
using BattleLib.Components.BattleState;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace BattleLib.GraphicComponents
{
    public class BattleGraphics : AbstractGraphicComponent, IBattleGraphicService
    {
        private readonly Dictionary<ClientIdentifier, PokemonDataView> dataViews = new Dictionary<ClientIdentifier, PokemonDataView>();
        private readonly Dictionary<ClientIdentifier, PokemonSprite> sprites = new Dictionary<ClientIdentifier, PokemonSprite>();

        private PokemonSprite aiSprite;
        private PokemonDataView aiView;
        private PokemonSprite playerSprite;
        private PokemonDataView playerView;

        public BattleGraphics(IPokeEngine game, ClientIdentifier player, ClientIdentifier ai)
            : base(game)
        {
            game.Services.AddService(typeof(IBattleGraphicService), this);

            aiView = new PokemonDataView(game, false);
            playerView = new PokemonDataView(game, true);
            aiSprite = new PokemonSprite(true, game);
            playerSprite = new PokemonSprite(false, game);

            dataViews[player] = playerView;
            dataViews[ai] = aiView;

            sprites[player] = playerSprite;
            sprites[ai] = aiSprite;

            foreach (var view in dataViews.Values)
                view.OnHPUpdated += delegate { OnHPSet(this, null); };

            foreach (var sprite in sprites.Values)
                sprite.OnPokemonAppeared += delegate { OnPokemonSet(this, null); };

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
            aiView.Setup();
            playerView.Setup();

            aiSprite.Setup();
            playerSprite.Setup();

            initAIGraphic();
            initPlayerGraphic();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            aiView.Draw(time, batch);
            playerView.Draw(time, batch);

            aiSprite.Draw(time, batch);
            playerSprite.Draw(time, batch);
        }

        private void initAIGraphic()
        {
            aiView.XPosition = Game.ScreenWidth * 0.2f;
            aiView.YPosition = Game.ScreenHeight * 0.1f;

            aiView.Height = Game.ScreenHeight * 0.25f;
            aiView.Width = Game.ScreenWidth * 0.3f;

            aiSprite.XPosition = Game.ScreenWidth * 0.6f;
            aiSprite.YPosition = Game.ScreenHeight * 0.1f;

            aiSprite.Height = Game.ScreenHeight * 0.25f;
            aiSprite.Width = Game.ScreenHeight * 0.25f;
        }

        private void initPlayerGraphic()
        {
            playerView.XPosition = Game.ScreenWidth * 0.6f;
            playerView.YPosition = Game.ScreenHeight * 0.4f;

            playerView.Height = Game.ScreenHeight * 0.25f;
            playerView.Width = Game.ScreenWidth * 0.3f;

            playerSprite.XPosition = Game.ScreenWidth * 0.2f;
            playerSprite.YPosition = Game.ScreenHeight * 0.4f;

            playerSprite.Height = Game.ScreenHeight * 0.25f;
            playerSprite.Width = Game.ScreenHeight * 0.25f;
        }
    }
}