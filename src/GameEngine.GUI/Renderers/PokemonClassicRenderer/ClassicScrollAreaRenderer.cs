using GameEngine.GUI.General;
using GameEngine.GUI.Panels;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Renderers
{
    public class ClassicScrollAreaRenderer : ScrollAreaRenderer
    {
        protected override void RenderComponent(ISpriteBatch spriteBatch, ScrollArea component)
        {
        }

        public override void RenderContent(ISpriteBatch batch, GameTime time, ScrollArea scrollArea)
        {
            var tmp = batch.GraphicsDevice.ScissorRectangle;
            batch.GraphicsDevice.ScissorRectangle = Rectangle.Intersect(tmp, scrollArea.Area);

            scrollArea.Content?.Draw(time, batch);

            batch.GraphicsDevice.ScissorRectangle = tmp;
        }
    }
}