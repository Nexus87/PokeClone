using GameEngine.GUI.Controlls;
using GameEngine.GUI.Graphics;

namespace GameEngine.GUI.Renderers
{
    public interface ITextAreaRenderer : IRenderer<TextArea>
    {
        int CharsPerLine(TextArea textArea);
        int LineSpacing { get; }
        int GetLineHeight(TextArea textArea);
    }
}