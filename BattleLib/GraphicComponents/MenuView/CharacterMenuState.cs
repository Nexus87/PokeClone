using BattleLib.Components.Input;
using BattleLib.Components.Menu;
using Microsoft.Xna.Framework;
using System;

namespace BattleLib.GraphicComponents.MenuView
{
    public class CharacterMenuState : AbstractMenuState
    {
        IMenuModel model;
        BattleGraphics graphics;

        String text;

        public CharacterMenuState(IMenuModel model, IMenuController controller, BattleGraphics graphics)
            : base(controller)
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

        public override void model_OnSelectionChanged(object sender, SelectedIndexChangedEvent e)
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
