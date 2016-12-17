using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics.Layouts
{
    public abstract class BoxLayout : AbstractLayout
    {
        private readonly NullGraphicObject spacer;
        private List<IGraphicComponent> internalComponants;

        public float Spacing
        {
            get
            {
                return spacer.Area.Width;
            }
            set
            {
                var area = new Rectangle(spacer.Area.X, spacer.Area.Y, (int) value, (int) value);
                spacer.Area = area;
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
            var totalFixedWidth = internalComponants.Where(c => c.HorizontalPolicy == ResizePolicy.Fixed).Sum(c => c.Area.Width);
            var extendingComponentsCount = internalComponants.Count(c => c.HorizontalPolicy == ResizePolicy.Extending);
            return Math.Max(0, Width - totalFixedWidth - totalPreferredWidth) / extendingComponentsCount;
            
        }

        protected float CalculateHeightForExtendingComponents()
        {
            var totalPreferredHeight = internalComponants.Where(c => c.VerticalPolicy == ResizePolicy.Preferred).Sum(c => c.PreferredHeight);
            var totalFixedHeight = internalComponants.Where(c => c.VerticalPolicy == ResizePolicy.Fixed).Sum(c => c.Area.Height);
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
            var location = component.Area.Location;
            var newHeight = component.Area.Height;
            var newWidth = component.Area.Width;
            if (component.VerticalPolicy == ResizePolicy.Preferred)
            {
                newHeight = (int) Math.Min(component.PreferredHeight, heightLimit);
            }
            else if (component.VerticalPolicy == ResizePolicy.Extending)
            {
                newHeight = (int) height;
            }

            if (component.HorizontalPolicy == ResizePolicy.Preferred)
                newWidth = (int) Math.Min(component.PreferredWidth, widthLimit);
            else if (component.HorizontalPolicy == ResizePolicy.Extending)
                newWidth = (int) width;

            var size = new Point(newWidth, newHeight);

            component.Area = new Rectangle(location, size);
        }

        private static void SetComponentPosition(IGraphicComponent component, float xPosition, float yPosition)
        {
            var location = new Point((int) xPosition, (int) yPosition);
            var size = component.Area.Size;
            component.Area = new Rectangle(location, size);
        }
    }
}