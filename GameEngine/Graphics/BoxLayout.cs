using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public class BoxLayout : AbstractLayout
    {
        public enum Direction { Horizontal, Vertical };

        private readonly List<IGraphicComponent> components = new List<IGraphicComponent>();
        private Direction direction;

        public BoxLayout(Direction direction)
        {
            this.direction = direction;
        }

        public override void AddComponent(IGraphicComponent component)
        {
            components.Add(component);
            Invalidate();
        }

        public override void Setup(ContentManager content)
        {
            foreach (var component in components)
                component.Setup(content);
        }

        protected override void DrawComponents(GameTime time, SpriteBatch batch)
        {
            foreach (var component in components)
                component.Draw(time, batch);
        }

        protected override void UpdateComponents()
        {
            if (direction == Direction.Horizontal)
            {
                float width = Width / components.Count;
                for (int i = 0; i < components.Count; i++)
                {
                    var component = components[i];
                    component.X = X + i * width;
                    component.Y = Y;
                    component.Width = width;
                    component.Height = Height;
                }
            }
            else
            {
                float height = Height / components.Count;
                for (int i = 0; i < components.Count; i++)
                {
                    var component = components[i];
                    component.X = X;
                    component.Y = Y + i * height;
                    component.Width = Width;
                    component.Height = height;
                }
            }
        }
    }

    public class VBoxLayout : BoxLayout
    {
        public VBoxLayout() : base(Direction.Vertical) { }
    }

    public class HBoxLayout : BoxLayout
    {
        public HBoxLayout() : base(Direction.Horizontal) { }
    }
}
