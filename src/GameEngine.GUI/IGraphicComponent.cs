using System.Collections.Generic;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI
{
    public interface IGraphicComponent : INode
    {

        IGraphicComponent Parent { get; set; }
        IEnumerable<IGraphicComponent> Children { get; }

        void Draw(GameTime time, ISpriteBatch spriteBatch);
        void Init();
    }

}