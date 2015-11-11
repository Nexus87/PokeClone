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
    public class DefaultLayout : ILayout
    {
        Vector2 position;
        Vector2 scale;

        int marginLeft;
        int marginRight;
        int marginTop;
        int marginBottom;

        IGraphicComponent component;

        public void Setup(ContentManager content)
        {
            if (component != null)
                component.Setup(content);
        }

        public void Draw(GameTime time, SpriteBatch batch)
        {
            if (component != null)
                component.Draw(time, batch);
        }

        public void Init(IGraphicComponent component)
        {
            position.X = component.X + marginLeft;
            position.Y = component.Y + marginTop;

            scale.X = component.Width - marginLeft - marginRight;
            scale.Y = component.Height - marginTop - marginBottom;

            SetComponentValues();
        }

        private void SetComponentValues()
        {
            if (component == null)
                return;

            component.X = position.X;
            component.Y = position.Y;

            component.Width = scale.X;
            component.Height = scale.Y;
        }

        public void AddComponent(IGraphicComponent component)
        {
            this.component = component;
            SetComponentValues();
        }


        public void SetMargin(int left = 0, int right = 0, int top = 0, int bottom = 0)
        {
            marginLeft = left;
            marginRight = right;
            marginTop = top;
            marginBottom = bottom;
        }
    }
}
