using System;
using System.Collections.Generic;
using System.Net;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics.NewGUI
{
    public interface IRenderer
    {
        void Render(ISpriteBatch spriteBatch, Rectangle parentConstraints);
    }

    public interface IGraphicComponent : IArea
    {
        IGraphicComponent Parent { get; }
        IEnumerable<IGraphicComponent> Children { get; }

        IRenderer Renderer { get; }
        void Update(GameTime time);
    }

}