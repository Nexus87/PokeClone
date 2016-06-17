using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public interface IScreen : IInputHandler
    {
        void Setup();
        void Draw(GameTime time, ISpriteBatch spriteBatch);
    }
}
