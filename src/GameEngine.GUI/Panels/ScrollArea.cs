using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.GUI.Renderers;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Panels
{
    [GameType]
    public class ScrollArea : AbstractPanel
    {
        private readonly ScrollAreaRenderer _renderer;
        private IGuiComponent _content;
        public bool Autoscrolling { get; set; }

        public ScrollArea(ScrollAreaRenderer renderer)
        {
            _renderer = renderer;
        }

        public override Rectangle Area
        {
            get { return base.Area; }
            set
            {
                MoveContent(value.Location - Area.Location);
                base.Area = value;
            }
        }

        public override void HandleKeyInput(CommandKeys key)
        {
            _content?.HandleKeyInput(key);
        }

        private void MoveContent(Point point)
        {
            _content?.SetPosition(_content.Area.Location + point);
        }

        public IGuiComponent Content
        {
            get { return _content; }
            set
            {
                if (_content != null)
                {
                    RemoveChild(_content);
                    _content.ComponentSelected -= ComponentOnComponentSelected;
                    _content.PreferredSizeChanged -= PreferredSizeChangedHandler;
                }

                _content = value;
                AddChild(_content);
                _content.ComponentSelected += ComponentOnComponentSelected;
                _content.PreferredSizeChanged += PreferredSizeChangedHandler;
                _content.SetPosition(this);
                _content.SetSize((int) _content.PreferredWidth, (int) _content.PreferredHeight);
            }
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
            else if (selectedArea.Bottom > Area.Bottom)
                newY = MoveUp(selectedArea);
            else
                newY = _content.Area.Y;

            _content.SetPosition(newX, newY);
        }

        private void PreferredSizeChangedHandler(object sender,
            GraphicComponentSizeChangedEventArgs graphicComponentSizeChangedEventArgs)
        {
            _content.SetSize((int) _content.PreferredWidth, (int) _content.PreferredHeight);
        }

        private int MoveUp(Rectangle selectedArea)
        {
            var diff = selectedArea.Bottom - Area.Bottom;
            return _content.Area.Y - diff;
        }

        private int MoveDown(Rectangle selectedArea)
        {
            var diff = selectedArea.Top - Area.Top;
            return _content.Area.Y - diff;
        }

        private int MoveRight(Rectangle selectedArea)
        {
            var diff = Area.Left - selectedArea.Left;
            return _content.Area.X + diff;
        }

        private int MoveLeft(Rectangle selectedArea)
        {
            var diff = selectedArea.Right - Area.Right;
            var newX = _content.Area.X - diff;
            return newX;
        }
    }
}