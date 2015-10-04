using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BattleLib.Components;
using Base;

namespace BattleLib.GraphicComponent
{
    public class AttackMenu : IMenuState
    {
        AttackMenuModel model;
        List<MenuItem> items = new List<MenuItem>();
        SpiteFont font;
        Texture2D arrow;

        public AttackMenu(AttackMenuModel model)
        {
            this.model = model;
        }

        public int SelectedIndex
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        void buildMenu(List<Move> moves)
        {
            foreach(var move in moves)
            {
                var item = new MenuItem(font, arrow, game);
            }
        }
        Rectangle constraints = new Rectangle();

        public void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeigth)
        {
            constraints.X = (int)(screenWidth / 7.0f);
            constraints.Y = (int)(2.0f * screenHeigth / 7.0f);

            constraints.Width = screenWidth - constraints.X;
            constraints.Height = screenHeigth - constraints.Y;

            throw new NotImplementedException();
        }
    }
}
