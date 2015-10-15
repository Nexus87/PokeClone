using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BattleLib.Components;
using Microsoft.Xna.Framework.Content;

namespace BattleLib.GraphicComponent
{
    public class SelectBox : IMenuState
    {
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
        MainMenuModel model;
        Rectangle Constraints = new Rectangle();
        
        public Point Size
        {
            get { return Constraints.Size; }
            set { Constraints.Size = value; }
        }

        public int SelectedIndex
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        void buildMenu(List<String> texts, MenuOrdering layout)
        {
            switch (layout)
            {
                case MenuOrdering.Table:
                    int i = 0;
                    foreach (var text in texts)
                    {
                        var item = new MenuItem(font, arrow);
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
        public SelectBox(MainMenuModel model)
        {
            this.model = model;

            model.OnSelectionChanged += Model_OnSelectionChanged;

        }

        private void Model_OnSelectionChanged(object sender, SelectionEventArgs e)
        {
            lastSelected.Selected = false;
            lastSelected = items[e.NewSelection];
            lastSelected.Selected = true;
        }

        public void Draw(Vector2 origin, SpriteBatch batch, GameTime time)
        {
            Constraints.Location = origin.ToPoint();
            batch.Draw(border, Constraints, Color.White);
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Draw(origin + offsets[i], batch, time);
            }
        }

        Point position;
        Point size;

        public void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeigth)
        {
            position.X = (int) (screenWidth / 2.0f);
            position.Y = (int)(2.0 * screenHeigth / 3.0f);

            size.X = screenWidth - position.X;
            size.Y = screenHeigth - position.Y;

            Constraints.Location = position;
            Constraints.Size = size;

            batch.Draw(border, Constraints, Color.White);
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Draw(position.ToVector2() + offsets[i], batch, time);
            }
        }


        public void Setup(ContentManager content)
        {
            this.font = content.Load<SpriteFont>("MenuFont");
            this.border = content.Load<Texture2D>("border");
            this.arrow = content.Load<Texture2D>("arrow");

            buildMenu(model.TextItems, MenuOrdering.Table);

            items[0].Selected = true;
            lastSelected = items[0];
        }
    }
}
