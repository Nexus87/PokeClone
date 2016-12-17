using GameEngine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using GameEngine.Globals;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;

namespace GameEngineTest.TestUtils
{
    public class GraphicComponentMock : IGraphicComponent
    {
        public bool WasDrawn { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public IEngineInterface Game { get; set; }

        public void PlayAnimation(IAnimation animation)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<GraphicComponentSizeChangedEventArgs> SizeChanged;
        public event EventHandler<GraphicComponentSizeChangedEventArgs> PreferredSizeChanged;

        public void RaisePreferredSizeChanged()
        {
            PreferredSizeChanged?.Invoke(this, new GraphicComponentSizeChangedEventArgs(this, 0, 0));
        }

        public Action DrawCallback = null;
        public void Draw(GameTime time, ISpriteBatch batch)
        {
            if (!_isVisible)
                return;
            WasDrawn = true;
            DrawCallback?.Invoke();

            if (batch is SpriteBatchMock)
            {
                batch.Draw(null, Area, Color);
            }
        }

        public void Setup()
        {

        }



        public Color Color { get; set; }

        public float PreferredHeight { get; set; }

        public float PreferredWidth { get; set; }

        public ResizePolicy HorizontalPolicy { get; set; }

        public ResizePolicy VerticalPolicy { get; set; }
        public Rectangle ScissorArea { get; set; }
        Rectangle IGraphicComponent.Area { get; set; }

        public Rectangle Area
        {
            get { return _area; }
            set
            {
                var sizeChanged = _area.Width != value.Width || _area.Height != value.Height;
                _area = value;

                if(sizeChanged)
                    OnSizeChanged();
            }
        }

        public IGraphicComponent Parent { get; set; }
        public IEnumerable<IGraphicComponent> Children { get; } = new List<IGraphicComponent>();
        public bool IsSelected { get; set; }
        public bool IsSelectable { get; set; }
        public virtual void HandleKeyInput(CommandKeys key)
        {
            throw new NotImplementedException();
        }

        protected void OnSizeChanged()
        {
            SizeChanged?.Invoke(this, new GraphicComponentSizeChangedEventArgs(this, Area.Width, Area.Height));
        }


        public void RaiseSizeChanged()
        {
            OnSizeChanged();
        }

        public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged = delegate { };
        private bool _isVisible = true;
        private Rectangle _area;

        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                if (_isVisible == value)
                    return;

                _isVisible = value;
                VisibilityChanged(this, new VisibilityChangedEventArgs(value));
            }
        }
    }
}
