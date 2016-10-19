using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using System;

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
            throw new System.NotImplementedException();
        }

        public event EventHandler<GraphicComponentSizeChangedEventArgs> SizeChanged;
        public event EventHandler<GraphicComponentPositionChangedEventArgs> PositionChanged;
        public event EventHandler<GraphicComponentSizeChangedEventArgs> PreferredSizeChanged;

        public void RaisePreferredSizeChanged()
        {
            PreferredSizeChanged(this, new GraphicComponentSizeChangedEventArgs(this, 0, 0));
        }

        public virtual float XPosition { get; set; }
        public virtual float YPosition { get; set; }

        public virtual float Width { get; set; }
        public virtual float Height { get; set; }

        public Action DrawCallback = null;
        public void Draw(GameTime time, ISpriteBatch batch)
        {
            if (!isVisible)
                return;
            WasDrawn = true;
            if (DrawCallback != null)
                DrawCallback();
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

        protected void OnSizeChanged()
        {
            if (SizeChanged != null)
                SizeChanged(this, new GraphicComponentSizeChangedEventArgs(this, Width, Height));
        }

        protected void OnPositionChanged()
        {
            if (PositionChanged != null)
                PositionChanged(this, new GraphicComponentPositionChangedEventArgs(XPosition, YPosition));
        }

        public void RaiseSizeChanged()
        {
            OnSizeChanged();
        }

        public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged = delegate { };
        private bool isVisible = true;

        public bool IsVisible
        {
            get
            {
                return isVisible;
            }
            set
            {
                if (isVisible == value)
                    return;

                isVisible = value;
                VisibilityChanged(this, new VisibilityChangedEventArgs(value));
            }
        }
    }
}
