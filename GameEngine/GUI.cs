using GameEngine.Graphics;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    public abstract class GUI : IGraphicComponent, IInputHandler
    {
        public event EventHandler OnCloseGUI = delegate { };
        public event EventHandler<GraphicComponentSizeChangedArgs> SizeChanged = delegate { };
        public event EventHandler<GraphicComponentPositionChangedArgs> PositionChanged = delegate { };

        public virtual float X { get; set; }
        public virtual float Y { get; set; }

        public virtual float Width { get; set; }

        public virtual float Height { get; set; }

        public abstract void Draw(GameTime time, ISpriteBatch batch);
        public abstract void Setup(ContentManager content);
        public abstract bool HandleInput(Keys key);
    }
}
