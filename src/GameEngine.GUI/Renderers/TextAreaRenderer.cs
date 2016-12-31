using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Renderers
{
    public abstract class TextAreaRenderer : AbstractRenderer<TextArea>
    {
        public abstract int CharsPerLine(TextArea textArea);
        public int LineSpacing { get; protected set; }
        public abstract int GetLineHeight(TextArea textArea);
    }
}