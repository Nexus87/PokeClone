using GameEngine.GUI.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics
{
    public class NullGraphicObject : AbstractGraphicComponent
    {
        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
        }
    }
}