using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Graphics.Widgets
{
    public interface IWidget : IInputHandler
    {
        event EventHandler<VisibilityChangedArgs> OnVisibilityChanged;

        bool IsVisible { get; }

        void Draw(GameTime time, ISpriteBatch batch);
    }

    public class VisibilityChangedArgs : EventArgs
    {
        public bool Visible { get; private set; }
        public VisibilityChangedArgs(bool visible)
        {
            Visible = visible;
        }
    }
}