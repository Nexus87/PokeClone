using Base;
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

        public BattleGraphics(PokeEngine game, ClientIdentifier player, ClientIdentifier ai)
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

        public override void Setup(ContentManager content)
        {
            aiView.Setup(content);
            playerView.Setup(content);

            aiSprite.Setup(content);
            playerSprite.Setup(content);

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
            aiView.X = (int)(PokeEngine.ScreenWidth * 0.2f);
            aiView.Y = (int)(PokeEngine.ScreenHeight * 0.1f);

            aiView.Height = (int)(PokeEngine.ScreenHeight * 0.25f);
            aiView.Width = (int)(PokeEngine.ScreenWidth * 0.3f);

            aiSprite.X = (int)(PokeEngine.ScreenWidth * 0.6f);
            aiSprite.Y = (int)(PokeEngine.ScreenHeight * 0.1f);

            aiSprite.Height = (int)(PokeEngine.ScreenHeight * 0.25f);
            aiSprite.Width = (int)(PokeEngine.ScreenHeight * 0.25f);
        }

        private void initPlayerGraphic()
        {
            playerView.X = (int)(PokeEngine.ScreenWidth * 0.6f);
            playerView.Y = (int)(PokeEngine.ScreenHeight * 0.4f);

            playerView.Height = (int)(PokeEngine.ScreenHeight * 0.25f);
            playerView.Width = (int)(PokeEngine.ScreenWidth * 0.3f);

            playerSprite.X = (int)(PokeEngine.ScreenWidth * 0.2f);
            playerSprite.Y = (int)(PokeEngine.ScreenHeight * 0.4f);

            playerSprite.Height = (int)(PokeEngine.ScreenHeight * 0.25f);
            playerSprite.Width = (int)(PokeEngine.ScreenHeight * 0.25f);
        }
    }
}