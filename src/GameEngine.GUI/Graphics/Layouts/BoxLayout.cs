using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Graphics.Layouts
{
    public abstract class BoxLayout : AbstractLayout
    {
        private readonly NullGraphicObject spacer;
        private List<IGraphicComponent> internalComponants;

        public float Spacing
        {
            get
            {
                return spacer.Width;
            }
            set
            {
                spacer.Width = spacer.Height = value;
            }
        }

        protected float HeightForExtendingComponents { get; set; }
        protected float WidthForExtendingComponents { get; set; }

        protected Func<IGraphicComponent, float> HeightModifier { get; set; }
        protected Func<IGraphicComponent, float> WidthModifier { get; set; }

        protected BoxLayout()
        {
            spacer = new NullGraphicObject { HorizontalPolicy = ResizePolicy.Fixed, VerticalPolicy = ResizePolicy.Fixed };
        }

        protected void SetComponents(IReadOnlyList<IGraphicComponent> components)
        {
            internalComponants = new List<IGraphicComponent>();
            if (components.Count == 0)
                return;

            internalComponants.Capacity = components.Count * 2;
            foreach (var c in components)
            {
                internalComponants.Add(c);
                internalComponants.Add(spacer);
            }
            internalComponants.RemoveAt(internalComponants.Count - 1);
        }


        protected float CalculateWidthForExtendingComponents()
        {
            var totalPreferredWidth = internalComponants.Where(c => c.HorizontalPolicy == ResizePolicy.Preferred).Sum(c => c.PreferredWidth);
            var totalFixedWidth = internalComponants.Where(c => c.HorizontalPolicy == ResizePolicy.Fixed).Sum(c => c.Width);
            var extendingComponentsCount = internalComponants.Count(c => c.HorizontalPolicy == ResizePolicy.Extending);
            return Math.Max(0, Width - totalFixedWidth - totalPreferredWidth) / extendingComponentsCount;
            
        }

        protected float CalculateHeightForExtendingComponents()
        {
            var totalPreferredHeight = internalComponants.Where(c => c.VerticalPolicy == ResizePolicy.Preferred).Sum(c => c.PreferredHeight);
            var totalFixedHeight = internalComponants.Where(c => c.VerticalPolicy == ResizePolicy.Fixed).Sum(c => c.Height);
            var extendingComponentsCount = internalComponants.Count(c => c.VerticalPolicy == ResizePolicy.Extending);
            return Math.Max(0, Height - totalFixedHeight - totalPreferredHeight) / extendingComponentsCount;

        }

        protected void LayoutComponents()
        {

            var totalWidthLeft = Width;
            var currentXPosition = XPosition;
            var totalHeightLeft = Height;
            var currentYPosition = YPosition;

            var upperXLimit = XPosition + Width;
            var upperYLimit = YPosition + Height;
            
            foreach(var component in internalComponants)
            {
                SetComponentPosition(component, currentXPosition, currentYPosition);
                SetSizeWithLimits(component, HeightForExtendingComponents, WidthForExtendingComponents, totalHeightLeft, totalWidthLeft);

                totalWidthLeft -= WidthModifier(component);
                currentXPosition += WidthModifier(component);

                totalHeightLeft -= HeightModifier(component);
                currentYPosition += HeightModifier(component);

                totalHeightLeft = Math.Max(0, totalHeightLeft);
                totalWidthLeft = Math.Max(0, totalWidthLeft);
                currentXPosition = Math.Min(upperXLimit, currentXPosition);
                currentYPosition = Math.Min(upperYLimit, currentYPosition);
            }
        }

        private static void SetSizeWithLimits(IGraphicComponent component, float height, float width, float heightLimit, float widthLimit)
        {
            if (component.VerticalPolicy == ResizePolicy.Preferred)
                component.Height = Math.Min(component.PreferredHeight, heightLimit);
            else if(component.VerticalPolicy == ResizePolicy.Extending)
                component.Height = height;

            if (component.HorizontalPolicy == ResizePolicy.Preferred)
                component.Width = Math.Min(component.PreferredWidth, widthLimit);
            else if (component.HorizontalPolicy == ResizePolicy.Extending)
                component.Width = width;
        }

        private static void SetComponentPosition(IGraphicComponent component, float xPosition, float yPosition)
        {
            component.XPosition = xPosition;
            component.YPosition = yPosition;
        }
    }
}