using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public interface ILayout
    {
        void Init(IGraphicComponent component);
        void Setup(ContentManager content);
        void Draw(GameTime time, SpriteBatch batch);
    }
}
