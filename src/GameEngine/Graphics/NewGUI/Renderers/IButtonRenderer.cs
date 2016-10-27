namespace GameEngine.Graphics.NewGUI.Renderers
{
    public interface IButtonRenderer : IRenderer
    {
        float PreferedWidth { get; }
        float PreferedHeight { get; }

        bool IsSelected { get; set; }
        string Text { get; set; }
        float TextHeight { get; set; }
    }
}