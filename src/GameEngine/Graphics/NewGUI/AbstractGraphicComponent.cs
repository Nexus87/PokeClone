using System;
using System.Collections.Generic;

namespace GameEngine.Graphics.NewGUI
{
    public abstract class AbstractGraphicComponent : IGraphicComponent
    {
        protected readonly List<IGraphicComponent> children;

        protected AbstractGraphicComponent()
        {
            children = new List<IGraphicComponent>();
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public abstract double CalculatePreferedWidth();
        public abstract double CalculatePreferedHeight();

        public double ParentWidthConstraint { get; set; }
        public double ParentHeightConstraint { get; set; }

        public event EventHandler<UpdateRequestedArgs> UpdateRequested;

        public IGraphicComponent Parent { get; protected set; }

        public IEnumerable<IGraphicComponent> Children
        {
            get { return children; }
        }

        public IDrawable Drawable { get; protected set; }

        public virtual void Update()
        {
            throw new NotImplementedException();
        }
    }
}