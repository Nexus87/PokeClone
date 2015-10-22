﻿using BattleLib.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.GraphicComponent
{
    public class CharacterMenuState : AbstractMenuState
    {
        CharacterMenuModel model;
        BattleGraphics graphics;

        String text;

        public CharacterMenuState(CharacterMenuModel model, BattleGraphics graphics)
            : base(model)
        {
            this.model = model;
            this.graphics = graphics;

            XPosition = 0;
            YPosition = 0;

            Width = 1.0f;
            Heigth = 2.0f/3.0f;

        }

        protected override void BuildMenu()
        {
            Vector2 margin = new Vector2(40, 40);
            Vector2 ySpacing = new Vector2(0, 45);
            int i = 0;

            foreach (var pkmn in model)
            {
                items.Add(new MenuItem(font, arrow) { Text = pkmn });
                itemOffsets.Add(margin + i * ySpacing);
                i++;
            }

            if (items.Count > 0)
                text = items[0].Text;
        }

        protected override void model_OnSelectionChanged(object sender, SelectionEventArgs e)
        {
            base.model_OnSelectionChanged(sender, e);
            graphics.DisplayText(currentItem.Text);
        }

        public override void OnHide()
        {
            graphics.ClearText();
        }
    }
}
