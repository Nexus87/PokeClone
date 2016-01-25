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

        public BattleGraphics()
        {
            aiView.OnHPUpdated += OnHPUpdated;
            playerView.OnHPUpdated += OnHPUpdated;
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
        }

        public void DisplayMessage(string text)
        {
            throw new NotImplementedException();
        }

        public void PlayAttackAnimation(bool player)
        {
            throw new NotImplementedException();
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
            if (player)
            {
                playerView.SetPokemon(pkmn);
            }
            else
            {
                aiView.SetPokemon(pkmn);
            }
        }
    }
}
