using Base;
using BattleLib.Components.BattleState;
using BattleLib.GraphicComponents.GUI;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Views;
using GameEngine.Graphics.Widgets;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BattleLib.GraphicComponents
{
    public class BattleGraphics : AbstractGraphicComponent
    {
        public event EventHandler OnRequestDone = delegate { };

        private PokemonDataView aiView;
        private PokemonDataView playerView;

        private PokemonSprite aiSprite;
        private PokemonSprite playerSprite;

        PokemonWrapper tmpPkmn;

        public BattleGraphics(PokeEngine game)
            : base(game)
        {
            aiView = new PokemonDataView(game);
            playerView = new PokemonDataView(game);
            aiSprite = new PokemonSprite(true, game);
            playerSprite = new PokemonSprite(false, game);
            aiView.OnHPUpdated += OnHPUpdated;
            playerView.OnHPUpdated += OnHPUpdated;

            aiSprite.OnAttackAnimationPlayed += OnHPUpdated;
            aiSprite.OnPokemonAppeared += aiSprite_OnPokemonAppeared;

            playerSprite.OnAttackAnimationPlayed += OnHPUpdated;
            playerSprite.OnPokemonAppeared += playerSprite_OnPokemonAppeared;
        }

        void aiSprite_OnPokemonAppeared(object sender, EventArgs e)
        {
            aiView.SetPokemon(tmpPkmn);
            tmpPkmn = null;
        }

        void playerSprite_OnPokemonAppeared(object sender, EventArgs e)
        {
            playerView.SetPokemon(tmpPkmn);
            tmpPkmn = null;
        }

        void OnHPUpdated(object sender, EventArgs e)
        {
            OnRequestDone(this, null);
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

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            aiView.Draw(time, batch);
            playerView.Draw(time, batch);

            aiSprite.Draw(time, batch);
            playerSprite.Draw(time, batch);
        }

        public void PlayAttackAnimation(bool player)
        {
            if (player)
                playerSprite.PlayAttackAnimation();
            else
                aiSprite.PlayAttackAnimation();
        }

        public void SetHP(bool player, int value)
        {
            if (player)
                playerView.SetHP(value);
            else
                aiView.SetHP(value);
        }

        public void ChangePkmn(bool player, PokemonWrapper pkmn)
        {
            tmpPkmn = pkmn;
            if (player)
            {
                playerSprite.SetPokemon(pkmn.ID);
            }
            else
            {
                aiSprite.SetPokemon(pkmn.ID);
            }
        }
    }
}
