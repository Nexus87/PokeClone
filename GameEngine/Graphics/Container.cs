﻿using GameEngine.Registry;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            components.Add(comp);
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