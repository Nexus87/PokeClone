﻿using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Graphics
{
    public interface IGraphicComponent
    {
        event EventHandler<EventArgs> SizeChanged;
        event EventHandler<EventArgs> PositionChanged;

        float X { get; set; }
        float Y { get; set; }

        float Width { get; set; }
        float Height { get; set; }

        void Draw(GameTime time, ISpriteBatch batch);
        void Setup(ContentManager content);
    }
}
