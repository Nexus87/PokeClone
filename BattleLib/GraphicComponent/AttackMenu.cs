using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BattleLib.Components;
using Base;
using Microsoft.Xna.Framework.Content;

namespace BattleLib.GraphicComponent
{
    public class AttackMenu : IMenuState
    {
        AttackMenuModel model;
        List<MenuItem> items = new List<MenuItem>();
        SpriteFont font;
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
                var item = new MenuItem(font, arrow);
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


        public void Setup(ContentManager content)
        {
            font = content.Load<SpriteFont>("MenuFont");
            arrow = content.Load<Texture2D>("Arrow");
        }
    }
}
