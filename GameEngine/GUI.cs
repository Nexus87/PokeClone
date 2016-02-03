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
        public GUI(Game game) : base(game) { }
        public event EventHandler OnCloseGUI = delegate { };
        public override event EventHandler<GraphicComponentSizeChangedArgs> SizeChanged = delegate { };
        public override event EventHandler<GraphicComponentPositionChangedArgs> PositionChanged = delegate { };

        public override float X { get; set; }
        public override float Y { get; set; }

        public override float Width { get; set; }
        public override float Height { get; set; }

        public abstract bool HandleInput(Keys key);
    }
}
