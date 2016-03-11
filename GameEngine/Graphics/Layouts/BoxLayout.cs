using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine.Graphics.Layouts
{
    public class BoxLayout : AbstractLayout
    {
        private Direction direction;

        public BoxLayout(Direction direction)
        {
            this.direction = direction;
        }

        protected enum Direction { Horizontal, Vertical };


        protected override void UpdateComponents(Container container)
        {
            var components = container.Components;
            if (direction == Direction.Horizontal)
            {
                float width = Width / components.Count;
                for (int i = 0; i < components.Count; i++)
                {
                    var component = components[i];
                    component.X = XPosition + i * width;
                    component.Y = YPosition;
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
                    component.X = XPosition;
                    component.Y = YPosition + i * height;
                    component.Width = Width;
                    component.Height = height;
                }
            }
        }
    }

    public class HBoxLayout : BoxLayout
    {
        public HBoxLayout()
            : base(Direction.Horizontal)
        {
        }
    }

    public class VBoxLayout : BoxLayout
    {
        public VBoxLayout()
            : base(Direction.Vertical)
        {
        }
    }
}