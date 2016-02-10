using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Basic
{
    public class Container : AbstractGraphicComponent
    {
        public Container(PokeEngine game) : base(game) { }
        public ILayout Layout { get; set; }

        public ReadOnlyCollection<IGraphicComponent> Components { get { return components.AsReadOnly(); } }
        protected List<IGraphicComponent> components = new List<IGraphicComponent>();

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

        public override void Setup(ContentManager content)
        {
            foreach (var c in components)
                c.Setup(content);
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
