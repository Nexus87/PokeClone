using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BattleLib.GraphicComponent
{
    internal class NullMenuState : IMenuState
    {
        public void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeigth)
        {   
        }

        public void Setup(ContentManager content)
        {
        }
    }
}
