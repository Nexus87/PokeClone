using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics.Basic
{
    public class NullGraphicObject : AbstractGraphicComponent
    {
        public override void Setup(ContentManager content)
        {
        }

        protected override void DrawComponent(GameTime time, SpriteBatch batch)
        {
        }
    }
}