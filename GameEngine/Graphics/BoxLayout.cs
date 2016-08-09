
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Graphics
{
    public abstract class BoxLayout : AbstractLayout
    {
        protected float HeightForExtendingComponents { get; set; }
        protected float WidthForExtendingComponents { get; set; }

        protected Func<IGraphicComponent, float> HeightModifier { get; set; }
        protected Func<IGraphicComponent, float> WidthModifier { get; set; }

        protected float CalculateWidthForExtendingComponents(List<IGraphicComponent> components)
        {
            float totalPreferredWidth = components.Where(c => c.HorizontalPolicy == ResizePolicy.Preferred).Sum(c => c.PreferredWidth);
            float totalFixedWidth = components.Where(c => c.HorizontalPolicy == ResizePolicy.Fixed).Sum(c => c.Width);
            int extendingComponentsCount = components.Where(c => c.HorizontalPolicy == ResizePolicy.Extending).Count();
            return Math.Max(0, Width - totalFixedWidth - totalPreferredWidth) / extendingComponentsCount;
            
        }

        protected float CalculateHeightForExtendingComponents(List<IGraphicComponent> components)
        {
            float totalPreferredHeight = components.Where(c => c.VerticalPolicy == ResizePolicy.Preferred).Sum(c => c.PreferredHeight);
            float totalFixedHeight = components.Where(c => c.VerticalPolicy == ResizePolicy.Fixed).Sum(c => c.Height);
            int extendingComponentsCount = components.Where(c => c.VerticalPolicy == ResizePolicy.Extending).Count();
            return Math.Max(0, Height - totalFixedHeight - totalPreferredHeight) / extendingComponentsCount;

        }

        protected void LayoutComponents(List<IGraphicComponent> components)
        {

            float totalWidthLeft = Width;
            float currentXPosition = XPosition;
            float totalHeightLeft = Height;
            float currentYPosition = YPosition;

            float upperXLimit = XPosition + Width;
            float upperYLimit = YPosition + Height;
            
            foreach(var component in components)
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

        private float RemoveFixedHeights(float height, List<IGraphicComponent> components)
        {
            float remainingHeight = height - components
                .Where(c => c.VerticalPolicy == ResizePolicy.Preferred)
                .Sum(c => c.PreferredHeight);

            return Math.Max(0, remainingHeight);
        }
    }

    public class HBoxLayout : BoxLayout
    {
        protected override void UpdateComponents(Container container)
        {
            var components = container.Components;

            HeightModifier = c => 0;
            WidthModifier = c => c.Width;
            HeightForExtendingComponents = Height;
            WidthForExtendingComponents = CalculateWidthForExtendingComponents(components);

            LayoutComponents(components);
        }
    }

    public class VBoxLayout : BoxLayout
    {
        protected override void UpdateComponents(Container container)
        {
            var components = container.Components;

            HeightModifier = c => c.Height;
            WidthModifier = c => 0;
            WidthForExtendingComponents = Width;
            HeightForExtendingComponents = CalculateHeightForExtendingComponents(components);

            LayoutComponents(components);
        }
    }
}