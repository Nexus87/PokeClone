using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using GameEngine.Graphics.Layouts;

namespace GameEngine.Graphics
{
    [GameType]
    public class Container : AbstractGraphicComponent
    {
        public ILayout Layout { get; set; }

        public IReadOnlyList<IGraphicComponent> Components { get { return components; } }
        private readonly List<IGraphicComponent> components = new List<IGraphicComponent>();

        public void AddComponent(IGraphicComponent comp)
        {
            comp.CheckNull("comp");
            RegisterEventHandler(comp);
            components.Add(comp);
            Invalidate();
        }

        private void RegisterEventHandler(IGraphicComponent comp)
        {
            comp.PreferredSizeChanged += PreferredSizeChangedHandler;
            comp.SizeChanged += SizeChangedHandler;
        }

        private void SizeChangedHandler(object sender, GraphicComponentSizeChangedEventArgs e)
        {
            if (ComponentResizePolicyIsFixed(e.Component))
                Invalidate();
        }

        private bool ComponentResizePolicyIsFixed(IGraphicComponent component)
        {
            return component.VerticalPolicy == ResizePolicy.Fixed
                || component.HorizontalPolicy == ResizePolicy.Fixed;
        }

        private void PreferredSizeChangedHandler(object sender, GraphicComponentSizeChangedEventArgs e)
        {
            if (ComponentResizePolicyIsPreferred(e.Component))
                Invalidate();
        }

        private static bool ComponentResizePolicyIsPreferred(IGraphicComponent e)
        {
            return e.VerticalPolicy == ResizePolicy.Preferred ||
                e.HorizontalPolicy == ResizePolicy.Preferred;
        }

        public void RemoveAllComponents()
        {
            components.ForEach(c => RemoveEventHandler(c));
            components.Clear();
            Invalidate();
        }

        private void RemoveEventHandler(IGraphicComponent c)
        {
            c.PreferredSizeChanged -= PreferredSizeChangedHandler;
            c.SizeChanged -= SizeChangedHandler;
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

        public void RemoveComponent(IGraphicComponent component)
        {
            if (!components.Remove(component))
                return;
            
            RemoveEventHandler(component);
            Invalidate();
        }
    }
}