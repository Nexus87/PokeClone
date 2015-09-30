using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BattleLib.Components;

namespace BattleLib.GraphicComponent
{
    public class SelectBox : AbstractGraphicComponent
    {
        readonly String[] mainMenu = { "Attack", "Item", "PKMN", "Escape" };
        readonly Vector2 margin = new Vector2(50, 40);
        readonly Vector2 xSpacing = new Vector2(150, 0);
        readonly Vector2 ySpacing = new Vector2(0, 50);
        readonly Vector2 offsetArrow = new Vector2(-20, 0);

        SpriteFont font;
        Texture2D border;
        Texture2D arrow;
        List<MenuItem> items = new List<MenuItem>();
        List<Vector2> offsets = new List<Vector2>();
        MenuItem lastSelected;
        MenuModel model;

        Rectangle Constraints = new Rectangle();
        public override Point Size
        {
            get { return Constraints.Size; }
            set { Constraints.Size = value; }
        }

        void buildMenu(List<String> texts, MenuOrdering layout)
        {
            switch (layout)
            {
                case MenuOrdering.Table:
                    int i = 0;
                    foreach (var text in mainMenu)
                    {
                        var item = new MenuItem(font, arrow, Game);
                        var x = (i % 2) * xSpacing; ;
                        var y = ((int)(i / 2.0f)) * ySpacing;
    
                        item.Text = text;
                        items.Add(item);
                        offsets.Add(x + y + margin);
                        i++;
                    }
                    break;

            }
        }
        public SelectBox(SpriteFont font, Texture2D border, Texture2D arrow, MenuModel model, Game game) : base(game)
        {
            this.model = model;
            this.font = font;
            this.border = border;
            this.arrow = arrow;

            model.OnSelectionChanged += Model_OnSelectionChanged;

            buildMenu(mainMenu.ToList(), MenuOrdering.Table);
            items[0].Selected = true;
            lastSelected = items[0];
        }

        private void Model_OnSelectionChanged(object sender, SelectionEventArgs e)
        {
            lastSelected.Selected = false;
            lastSelected = items[e.NewSelection];
            lastSelected.Selected = true;
        }

        public override void Draw(Vector2 origin, SpriteBatch batch, GameTime time)
        {
            Constraints.Location = origin.ToPoint();
            batch.Draw(border, Constraints, Color.White);
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Draw(origin + offsets[i], batch, time);
            }
        }

        public override void Setup(Rectangle screen)
        {
        }
    }
}
