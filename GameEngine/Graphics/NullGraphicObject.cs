using Microsoft.Xna.Framework;

namespace GameEngine.Graphics
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