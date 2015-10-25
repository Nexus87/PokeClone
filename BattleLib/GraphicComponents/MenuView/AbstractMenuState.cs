using BattleLib.Components;
using BattleLib.Components.Input;
using BattleLib.Components.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.GraphicComponent
{
    public abstract class AbstractMenuState : IMenuState
    {
        public float XPosition { get; set; }
        public float YPosition { get; set; }

        public float Width { get; set; }
        public float Heigth { get; set; }

        protected List<MenuItem> items = new List<MenuItem>();
        protected List<Vector2> itemOffsets = new List<Vector2>();
        protected MenuItem currentItem;

        private Point position;
        private Point size;

        private Rectangle constraints = new Rectangle();

        protected Texture2D border;
        protected Texture2D arrow;
        protected SpriteFont font;

        protected abstract void BuildMenu();

        protected AbstractMenuState(IMenuController controller)
        {
            controller.OnSelectedIndexChange += model_OnSelectionChanged;
        }

        protected AbstractMenuState() {}

        public virtual void model_OnSelectionChanged(object sender, SelectedIndexChangedEvent e)
        {
            if (e.NewSelection >= items.Count)
                return;

            if (currentItem != null)
                currentItem.Selected = false;

            currentItem = items[e.NewSelection];
            currentItem.Selected = true;
        }

        public virtual void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeigth)
        {
            position.X = (int)(screenWidth * XPosition);
            position.Y = (int)(screenHeigth * YPosition);

            size.X = (int) (screenWidth * Width);
            size.Y = (int)(screenHeigth * Heigth);

            constraints.Location = position;
            constraints.Size = size;

            batch.Draw(border, constraints, Color.White);
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Draw(position.ToVector2() + itemOffsets[i], batch, time);
            }
        }

        public virtual void Setup(ContentManager content)
        {
            font = content.Load<SpriteFont>("MenuFont");
            arrow = content.Load<Texture2D>("arrow");
            border = content.Load<Texture2D>("border");

            BuildMenu();

            if (currentItem == null && items.Count > 0)
            {
                currentItem = items[0];
                currentItem.Selected = true;
            }
        }


        public virtual void OnShow()
        { }

        public virtual void OnHide()
        { }
    }
}
