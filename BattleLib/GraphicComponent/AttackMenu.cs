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
    public class AttackMenu : AbstractMenuState
    {
        AttackMenuModel model;

        public AttackMenu(AttackMenuModel model) : base(model)
        {
            this.model = model;

            XPosition = 1.0f / 2.0f;
            YPosition = 2.0f / 3.0f;

            Width = 1.0f - XPosition;
            Heigth = 1.0f - YPosition;
        }

        protected override void BuildMenu()
        {
            Vector2 margin = new Vector2(50, 40);
            Vector2 ySpacing = new Vector2(0, 25);


            for (int i = 0; i < 4; i++)
            {
                itemOffsets.Add(margin + i * ySpacing);
            }

            foreach (var move in model)
            {
                var item = new MenuItem(font, arrow);
                item.Text = move;
                items.Add(item);
            }

            while (items.Count < 4)
            {
                var item = new MenuItem(font, arrow);
                item.Text = "--------------";
                items.Add(item);
            }
        }

    }
}
