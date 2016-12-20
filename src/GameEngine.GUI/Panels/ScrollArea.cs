using GameEngine.GUI.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Panels
{
    public class ScrollArea : AbstractPanel
    {
        private IGraphicComponent _content;
        public bool Autoscrolling { get; set; }

        public void SetContent(IGraphicComponent component)
        {
            if (_content != null)
            {
                RemoveChild(_content);
                _content.ComponentSelected -= ComponentOnComponentSelected;
            }

            _content = component;
            AddChild(component);
            component.ComponentSelected += ComponentOnComponentSelected;
        }

        private void ComponentOnComponentSelected(object sender, ComponentSelectedEventArgs componentSelectedEventArgs)
        {
            var selectedArea = componentSelectedEventArgs.Source.Area;
            int newX;
            int newY;
            if (selectedArea.Right > Area.Right)
                newX = MoveLeft(selectedArea);
            else if (selectedArea.Left < Area.Left)
                newX = MoveRight(selectedArea);
            else
                newX = _content.Area.X;

            if (selectedArea.Top < Area.Top)
                newY = MoveDown(selectedArea);
            else if (selectedArea.Bottom < Area.Bottom)
                newY = MoveUp(selectedArea);
            else
                newY = _content.Area.Y;

            SetPosition(newX, newY);
        }

        private void SetPosition(int newX, int newY)
        {
            _content.Area = new Rectangle(new Point(newX, newY), _content.Area.Size);
        }

        private int MoveUp(Rectangle selectedArea)
        {
            var diff = selectedArea.Bottom - Area.Bottom;
            return _content.Area.Y - diff;
        }

        private int MoveDown(Rectangle selectedArea)
        {
            var diff = Area.Top - selectedArea.Top;
            return _content.Area.Y - diff;
        }

        private int MoveRight(Rectangle selectedArea)
        {
            var diff = Area.Left - selectedArea.Left;
            return _content.Area.X - diff;
        }

        private int MoveLeft(Rectangle selectedArea)
        {
            var diff = selectedArea.Right - Area.Right;
            var newX = _content.Area.X - diff;
            return newX;
        }
    }
}