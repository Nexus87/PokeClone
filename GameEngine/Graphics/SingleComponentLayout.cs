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
    public class SingleComponentLayout : AbstractLayout
    {
        IGraphicComponent component;

        public override void Setup(ContentManager content)
        {
            if (component != null)
                component.Setup(content);
        }

        public override void AddComponent(IGraphicComponent component)
        {
            this.component = component;
            Invalidate();
        }

        protected override void DrawComponents(GameTime time, SpriteBatch batch)
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
