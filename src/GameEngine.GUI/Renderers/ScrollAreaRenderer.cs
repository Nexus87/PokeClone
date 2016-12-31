using GameEngine.GUI.General;
using GameEngine.GUI.Panels;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Renderers
{
    public abstract class ScrollAreaRenderer : AbstractRenderer<ScrollArea>
    {
        public abstract void RenderContent(ISpriteBatch batch, GameTime time, ScrollArea scrollArea);
    }
}