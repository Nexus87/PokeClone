using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics.Layouts
{
    public class SingleComponentLayout : AbstractLayout
    {
        private IGraphicComponent component;

        public override void AddComponent(IGraphicComponent component)
        {
            this.component = component;
            Invalidate();
        }

        public override void RemoveComponent(IGraphicComponent component)
        {
            if (this.component == component)
                this.component = null;
            // No need for invalidation here
        }

        public override void Setup(ContentManager content)
        {
            if (component != null)
                component.Setup(content);
        }

        protected override void DrawComponents(GameTime time, ISpriteBatch batch)
        {
            if (component != null)
                component.Draw(time, batch);
        }

        protected override void UpdateComponents()
        {
            if (component == null)
                return;

            component.X = X;
            component.Y = Y;

            component.Width = Width;
            component.Height = Height;
        }
    }
}