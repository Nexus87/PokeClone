using GameEngine.GUI.Components;
using GameEngine.GUI.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Renderers.PokemonClassicRenderer
{
    public class ClassicLineRenderer : AbstractRenderer<HpLine>, IHpLineRenderer
    {
        private readonly ITexture2D _cup;
        private readonly ITexture2D _pixel;
        private readonly Color _backgroundColor;
        private const float RelativeBorderSize = 0.2f;

        public ClassicLineRenderer(ITexture2D cup, ITexture2D pixel, Color backgroundColor)
        {
            _cup = cup;
            _pixel = pixel;
            _backgroundColor = backgroundColor;
        }

        public override void Render(ISpriteBatch spriteBatch, HpLine component)
        {
            var scale = component.MaxHp == 0 ? 0 : component.Current / ((float)component.MaxHp);

            RenderLine(spriteBatch, component.Area, Color.Black);
            RenderLine(spriteBatch, BackgroundArea(component.Area), _backgroundColor);
            RenderLine(spriteBatch, InnerArea(component.Area, scale), component.Color);
        }

        private static Rectangle BackgroundArea(Rectangle area)
        {
            var borderSize = (int) (area.Height * RelativeBorderSize);
            var position = area.Location + new Point(borderSize, borderSize);
            var size = new Point(area.Width - 2 * borderSize, area.Height - 2*borderSize);

            return new Rectangle(position, size);
        }

        private static Rectangle InnerArea(Rectangle area, float factor)
        {
            var borderSize = (int) (area.Height * RelativeBorderSize);
            var position = area.Location + new Point(borderSize, borderSize);
            var size = new Point((int) ((area.Width - 2 * borderSize) * factor), area.Height - 2*borderSize);

            return new Rectangle(position, size);
        }

        private void RenderLine(ISpriteBatch batch, Rectangle area, Color color)
        {
            if(area.Width < area.Height)
                return;
            
            var cupWidth = area.Height;
            var leftCupPosition = area.Location;
            var rightCupPosition = new Point(area.Right - cupWidth, area.Top);
            var cupSize = new Point(cupWidth, cupWidth);
            var lineArea = new Rectangle(area.Left + cupWidth / 2, area.Top, area.Width - cupWidth, area.Height);

            RenderImage(batch, _pixel, lineArea, color);
            RenderImage(batch, _cup, new Rectangle(leftCupPosition, cupSize));
            RenderImage(batch, _cup, new Rectangle(rightCupPosition, cupSize));
        }
    }
}