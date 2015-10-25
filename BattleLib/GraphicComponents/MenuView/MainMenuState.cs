using BattleLib.Components.Input;
using BattleLib.Components.Menu;
using Microsoft.Xna.Framework;

namespace BattleLib.GraphicComponents.MenuView
{
    public class MainMenuState : AbstractMenuState
    {
        IMenuModel model;

        protected override void BuildMenu()
        {
            Vector2 margin = new Vector2(50, 40);
            Vector2 xSpacing = new Vector2(150, 0);
            Vector2 ySpacing = new Vector2(0, 50);
            int i = 0;

            foreach (var text in model)
            {
                items.Add(new MenuItem(font, arrow) { Text = text });

                var x = (i % 2) * xSpacing; ;
                var y = ((int)(i / 2.0f)) * ySpacing;

                itemOffsets.Add(x + y + margin);
                i++;
            }

        }
        public MainMenuState(IMenuModel model, IMenuController controller) : base(controller)
        {
            this.model = model;

            XPosition = 1.0f / 2.0f;
            YPosition = 2.0f / 3.0f;

            Width = 1.0f - XPosition;
            Heigth = 1.0f - YPosition;
        }
    }
}
