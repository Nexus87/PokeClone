
namespace GameEngine.Graphics
{
    public class BoxLayout : AbstractLayout
    {
        private Direction direction;

        protected BoxLayout(Direction direction)
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
                    component.XPosition = XPosition + i * width;
                    component.YPosition = YPosition;
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
                    component.XPosition = XPosition;
                    component.YPosition = YPosition + i * height;
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