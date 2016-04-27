using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics.Basic
{
    public class NullGraphicObject : AbstractGraphicComponent
    {
        public NullGraphicObject(IPokeEngine game) : base(game) { }

        public override void Setup()
        {
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
        }
    }
}