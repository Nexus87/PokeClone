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
        public PokeEngine Game { get; protected set; }
        public GUI(PokeEngine game) { Game = game; }
        public event EventHandler OnCloseGUI = delegate { };
        public event EventHandler<GraphicComponentSizeChangedArgs> SizeChanged = delegate { };
        public event EventHandler<GraphicComponentPositionChangedArgs> PositionChanged = delegate { };

        public float X { get; set; }
        public float Y { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }

        public abstract bool HandleInput(Keys key);
        public abstract void Setup(ContentManager content);
        public abstract void Draw(GameTime time, ISpriteBatch batch);
    }
}
