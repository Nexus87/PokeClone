using Base;
using BattleLib.Components.BattleState;
using BattleLib.GraphicComponents.GUI;
using BattleLib.GraphicComponents.MenuView;
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

        private PokemonDataView aiView = new PokemonDataView();
        private PokemonDataView playerView = new PokemonDataView();

        private PokemonSprite aiSprite = new PokemonSprite(true);
        private PokemonSprite playerSprite = new PokemonSprite(false);

        PokemonWrapper tmpPkmn;

        public BattleGraphics()
        {
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
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            aiView.Draw(time, batch);
            playerView.Draw(time, batch);

            aiSprite.Draw(time, batch);
            playerSprite.Draw(time, batch);
        }

        public void DisplayMessage(string text)
        {
            throw new NotImplementedException();
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
