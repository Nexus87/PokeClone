using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine.Graphics
{
    public class Container : AbstractGraphicComponent
    {
        public ILayout Layout { get; set; }

        public IReadOnlyList<IGraphicComponent> Components { get { return components.AsReadOnly(); } }
        private readonly List<IGraphicComponent> components = new List<IGraphicComponent>();

        public void AddComponent(IGraphicComponent comp, int index)
        {
            components.Insert(index, comp);
            Invalidate();
        }

        public void AddComponent(IGraphicComponent comp)
        {
            components.Add(comp);
            Invalidate();
        }

        public void RemoveComponent(IGraphicComponent comp)
        {
            components.Remove(comp);
            Invalidate();
        }

        public void RemoveAllComponents()
        {
            components.Clear();
            Invalidate();
        }

        public void ForceLayout()
        {
            Layout.LayoutContainer(this);
        }

        public override void Setup()
        {
            foreach (var c in components)
                c.Setup();
        }

        protected override void Update()
        {
            Layout.LayoutContainer(this);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            foreach (var c in components)
                c.Draw(time, batch);
        }
    }
}