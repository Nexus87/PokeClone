using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using System;
using GameEngine.Graphics.General;

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

        public virtual float XPosition { get; set; }
        public virtual float YPosition { get; set; }

        public virtual float Width { get; set; }
        public virtual float Height { get; set; }

        public Action DrawCallback = null;
        public void Draw(GameTime time, ISpriteBatch batch)
        {
            if (!_isVisible)
                return;
            WasDrawn = true;
            DrawCallback?.Invoke();

            if (batch is SpriteBatchMock)
            {
                batch.Draw(null, new Rectangle((int)XPosition, (int)YPosition, (int)Width, (int)Height), Color);
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
        public Rectangle Area => new Rectangle((int) XPosition, (int) YPosition, (int) Width, (int) Height);

        protected void OnSizeChanged()
        {
            SizeChanged?.Invoke(this, new GraphicComponentSizeChangedEventArgs(this, Width, Height));
        }


        public void RaiseSizeChanged()
        {
            OnSizeChanged();
        }

        public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged = delegate { };
        private bool _isVisible = true;

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
