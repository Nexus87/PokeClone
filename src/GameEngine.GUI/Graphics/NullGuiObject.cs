using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics
{
    public class NullGuiObject : AbstractGuiComponent
    {
        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
        }
    }
}