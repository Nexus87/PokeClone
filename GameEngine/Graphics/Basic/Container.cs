using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Basic
{
    public class Container : AbstractGraphicComponent
    {
        protected List<IGraphicComponent> components = new List<IGraphicComponent>();

        protected void AddComponent(IGraphicComponent comp)
        {
            components.Add(comp);
        }

        protected void RemoveComponent(IGraphicComponent comp)
        {
            components.Remove(comp);
        }

        public override void Setup(ContentManager content)
        {
            foreach (var c in components)
                c.Setup(content);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            foreach (var c in components)
                c.Draw(time, batch);
        }
    }
}
