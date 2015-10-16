using BattleLib.Components;
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

        Rectangle textBox = new Rectangle();
        String text;

        public CharacterMenuState(CharacterMenuModel model)
            : base(model)
        {
            this.model = model;
            
            XPosition = 0;
            YPosition = 0;

            Width = 1.0f;
            Heigth = 0.75f;

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

        public override void Draw(GameTime time, Microsoft.Xna.Framework.Graphics.SpriteBatch batch, int screenWidth, int screenHeigth)
        {
            base.Draw(time, batch, screenWidth, screenHeigth);

            textBox.X = 0;
            textBox.Y = (int) (Heigth * screenHeigth);
            textBox.Width = screenWidth;
            textBox.Height = screenHeigth - textBox.Y;

            batch.Draw(border, textBox, Color.White);
            batch.DrawString(font, text, textBox.Location.ToVector2() + new Vector2(40, 40), Color.Black);
        }

        protected override void model_OnSelectionChanged(object sender, SelectionEventArgs e)
        {
            base.model_OnSelectionChanged(sender, e);
            text = currentItem.Text;
        }
    }
}
