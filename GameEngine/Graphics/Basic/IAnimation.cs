using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Basic
{
    public interface IAnimation
    {
        event EventHandler AnimationFinished;
        void Update(GameTime time, IGraphicComponent component);
    }
}
