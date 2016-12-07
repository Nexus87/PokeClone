namespace GameEngine.GUI
{
    public interface IGuiComponent : IGraphicComponent
    {
        bool IsSelectable { get; set; }
        void HandleKeyInput(CommandKeys key);
        bool IsSelected { get; set; }
    }
}