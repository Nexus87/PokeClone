﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public interface IGraphicComponent
    {
        Game Game { get; }
        Point Size { get; set; }

        void Setup(Rectangle screen);
        void Draw(Vector2 origin, SpriteBatch batch, GameTime time);
    }

    public abstract class AbstractGraphicComponent : IGraphicComponent
    {
        protected AbstractGraphicComponent(Game game)
        {
            Game = game;
        }

        public Game Game { get; protected set; }
        public virtual Point Size { get; set; }
        public abstract void Draw(Vector2 origin, SpriteBatch batch, GameTime time);
        public abstract void Setup(Rectangle screen);
    }
}