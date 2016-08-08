
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Graphics
{
    public class BoxLayout : AbstractLayout
    {
        private Direction direction;

        protected float HeightForExtendingComponents { get; set; }
        protected float WidthForExtendingComponents { get; set; }

        protected void CalculateSizeForExtendingComponents(List<IGraphicComponent> components)
        {
            var fixedWidthComponents = components.Where(c => c.HorizontalPolicy == ResizePolicy.Fixed);
            var fixedHeightComponents = components.Where(c => c.VerticalPolicy == ResizePolicy.Fixed);

            float totalFixedWidth = fixedWidthComponents.Sum(c => c.PreferedWidth);
            float totalFixedHeight = fixedHeightComponents.Sum(c => c.PreferedHeight);

            var extendingWidthComponetns = components.Count - fixedWidthComponents.Count();
            var extendingHeightComponetns = components.Count - fixedHeightComponents.Count();

            HeightForExtendingComponents = Math.Max(0, Height - totalFixedHeight) / extendingHeightComponetns;
            WidthForExtendingComponents = Math.Max(0, Width - totalFixedWidth) / extendingWidthComponetns;
            
        }
        
        protected BoxLayout(Direction direction)
        {
            this.direction = direction;
        }

        protected enum Direction { Horizontal, Vertical };


        protected override void UpdateComponents(Container container)
        {
            var components = container.Components;
            CalculateSizeForExtendingComponents(components);

            if (direction == Direction.Horizontal)
            {
                float totalWidthLeft = Width;
                float currentXPosition = XPosition;

                for (int i = 0; i < components.Count; i++)
                {
                    var component = components[i];
                    SetComponentPosition(component, currentXPosition, YPosition);
                    SetSizeWithLimits(component, Height, WidthForExtendingComponents, Height, totalWidthLeft);

                    totalWidthLeft -= component.Width;
                    currentXPosition += component.Width;
                }
            }
            else
            {
                float totalHeightLeft = Height;
                float currentYPosition = YPosition;

                for (int i = 0; i < components.Count; i++)
                {
                    var component = components[i];
                    SetComponentPosition(component, XPosition, currentYPosition);
                    SetSizeWithLimits(component, HeightForExtendingComponents, Width, totalHeightLeft, Width);

                    totalHeightLeft -= component.Height;
                    currentYPosition += component.Height;
                }
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
        public HBoxLayout()
            : base(Direction.Horizontal)
        {
        }
    }

    public class VBoxLayout : BoxLayout
    {
        public VBoxLayout()
            : base(Direction.Vertical)
        {
        }
    }
}