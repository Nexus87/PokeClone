
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
            var fixedWidthComponents = components.Where(c => c.HorizontalPolicy == ResizePolicy.Fixed);
            float totalFixedWidth = fixedWidthComponents.Sum(c => c.PreferedWidth);
            var extendingWidthComponetns = components.Count - fixedWidthComponents.Count();
            return Math.Max(0, Width - totalFixedWidth) / extendingWidthComponetns;
            
        }

        protected float CalculateHeightForExtendingComponents(List<IGraphicComponent> components)
        {
            var fixedHeightComponents = components.Where(c => c.VerticalPolicy == ResizePolicy.Fixed);
            float totalFixedHeight = fixedHeightComponents.Sum(c => c.PreferedHeight);
            var extendingHeightComponetns = components.Count - fixedHeightComponents.Count();
            return Math.Max(0, Height - totalFixedHeight) / extendingHeightComponetns;

        }

        protected void LayoutComponents(List<IGraphicComponent> components)
        {

            float totalWidthLeft = Width;
            float currentXPosition = XPosition;
            float totalHeightLeft = Height;
            float currentYPosition = YPosition;

            foreach(var component in components)
            {
                SetComponentPosition(component, currentXPosition, currentYPosition);
                SetSizeWithLimits(component, HeightForExtendingComponents, WidthForExtendingComponents, totalHeightLeft, totalWidthLeft);

                totalWidthLeft -= WidthModifier(component);
                currentXPosition += WidthModifier(component);

                totalHeightLeft -= HeightModifier(component);
                currentYPosition += HeightModifier(component);
            }
        }

        private static void SetSizeWithLimits(IGraphicComponent component, float height, float width, float heightLimit, float widthLimit)
        {
            if (component.VerticalPolicy == ResizePolicy.Fixed)
                component.Height = Math.Min(component.PreferedHeight, heightLimit);
            else
                component.Height = height;

            if (component.HorizontalPolicy == ResizePolicy.Fixed)
                component.Width = Math.Min(component.PreferedWidth, widthLimit);
            else
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
                .Where(c => c.VerticalPolicy == ResizePolicy.Fixed)
                .Sum(c => c.PreferedHeight);

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