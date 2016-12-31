using System;
using GameEngine.GUI.General;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Panels;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicSelectablePanelRenderer : AbstractRenderer<SelectablePanel>, ISelectablePanelRenderer
    {
        private readonly ITexture2D _arrow;
        private const int DefaultArrowSize = 32;
        public ClassicSelectablePanelRenderer(ITexture2D arrow)
        {
            _arrow = arrow;
        }

        public override void Render(ISpriteBatch spriteBatch, SelectablePanel component)
        {
            if(!component.IsSelected)
                return;

            var arrowSize = ArrowSize(component.Area);
            var arrowPosition = ArrowPosition(component.Area, arrowSize);
            RenderImage(spriteBatch, _arrow, new Rectangle(arrowPosition, new Point(arrowSize, arrowSize)));
        }

        private Point ArrowPosition(Rectangle componentArea, int arrowSize)
        {
            var remainingHeight = componentArea.Height - arrowSize;
            return new Point(componentArea.X, componentArea.Y + (remainingHeight / 2));
        }

        public Rectangle GetContentArea(SelectablePanel selectablePanel)
        {
            var panelArea = selectablePanel.Area;
            var arrowWidth = ArrowSize(panelArea);
            return new Rectangle(panelArea.X + arrowWidth, panelArea.Y, panelArea.Width - arrowWidth, panelArea.Height);
        }

        private static int ArrowSize(Rectangle panelArea)
        {
            var arrowSize = Math.Min(DefaultArrowSize, panelArea.Height);
            return panelArea.Width > arrowSize ? arrowSize : 0;
        }
    }
}