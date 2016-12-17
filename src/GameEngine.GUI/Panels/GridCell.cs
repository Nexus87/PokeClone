using System;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Panels
{
    internal class GridCell
    {
        public EventHandler PreferredSizeChanged;
        private IGraphicComponent _guiComponent;

        public IGraphicComponent GuiComponent
        {
            get { return _guiComponent; }
            set
            {
                if (_guiComponent != null)
                    _guiComponent.PreferredSizeChanged -= PreferredSizeChangedHandler;

                _guiComponent = value;
                if (value != null)
                    value.PreferredSizeChanged += PreferredSizeChangedHandler;
            }
        }

        private void PreferredSizeChangedHandler(object sender, GraphicComponentSizeChangedEventArgs e)
        {
            PreferredSizeChanged?.Invoke(this, e);
        }

        public void SetConstraints(Rectangle constraints, Rectangle gridConstraints)
        {
            if (GuiComponent == null)
                return;
            GuiComponent.Area = constraints;
            GuiComponent.ScissorArea = GuiComponent.ScissorArea = Rectangle.Intersect(constraints, gridConstraints);
        }

        public float PreferedWidth => GuiComponent?.PreferredWidth ?? 0;
        public float PreferedHeight => GuiComponent?.PreferredHeight ?? 0;

        public bool IsSelectable => GuiComponent?.IsSelectable ?? false;

        public bool IsSelected
        {
            get { return GuiComponent.IsSelected; }
            set { GuiComponent.IsSelected = value; }
        }

        public void Draw(GameTime time, ISpriteBatch spriteBatch)
        {
            GuiComponent?.Draw(time, spriteBatch);
        }

        public void Setup()
        {
            GuiComponent?.Setup();
        }
    }
}