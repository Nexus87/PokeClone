using BattleLib.Components.Input;
using BattleLib.Components.Menu;
using Microsoft.Xna.Framework;

namespace BattleLib.GraphicComponents.MenuView
{
    public class AttackMenu : AbstractMenuState
    {
        IMenuModel model;

        public AttackMenu(IMenuModel model, IMenuController controller) : base(controller)
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