using GameEngine.GUI.Controlls;
using GameEngine.GUI.Graphics.General;

namespace GameEngine.GUI.Renderers
{
    public interface ILabelRenderer : IRenderer<Label>
    {
        float GetPreferedWidth(Label label);
        float GetPreferedHeight(Label label);
    }

    public class ClassicLabelRenderer : AbstractRenderer<Label>, ILabelRenderer
    {
        private readonly ISpriteFont _font;

        public ClassicLabelRenderer(ISpriteFont font)
        {
            _font = font;
        }

        public override void Render(ISpriteBatch spriteBatch, Label label)
        {
            RenderText(spriteBatch, _font, label.Text, label.Area, label.TextSize);
        }

        public float GetPreferedWidth(Label label)
        {
            var scale = label.TextSize / _font.MeasureString(" ").Y;
            return scale * _font.MeasureString(label.Text).X;
        }

        public float GetPreferedHeight(Label label)
        {
            return label.TextSize;
        }
    }
}