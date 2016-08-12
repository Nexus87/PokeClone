using BattleLib.Components.BattleState;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class HPText : AbstractGraphicComponent
    {
        private TextBox text;
        private string maxHP;

        public HPText(TextBox text)
        {
            this.text = text;
        }

        public override float PreferredHeight
        {
            get
            {
                return text.PreferredHeight;
            }
        }

        public override float PreferredWidth
        {
            get
            {
                return text.PreferredWidth;
            }
        }

        public float PreferredTextHeight
        {
            get
            {
                return text.PreferredTextHeight;
            }

            set{
                text.PreferredTextHeight = value;
            }
        }
        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            text.Draw(time, batch);
        }


        public void SetPokemon(PokemonWrapper pokemon)
        {
            maxHP = "/" + pokemon.MaxHP.ToString().PadLeft(3, ' ');
            SetHP(pokemon.HP);
            
        }

        protected override void Update()
        {
            text.SetCoordinates(this);
        }
        public void SetHP(int hp)
        {
            text.Text = hp.ToString().PadLeft(3, ' ') + maxHP;
        }
        public override void Setup()
        {
            text.Setup();
        }
    }
}
