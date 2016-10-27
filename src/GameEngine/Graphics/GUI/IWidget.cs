namespace GameEngine.Graphics.GUI
{
    /// <summary>
    /// A widget is a GrapicComponent, that can handle user input.
    /// </summary>
    /// <remarks>
    /// These classes are mainly used for building GUIs.
    /// </remarks>
    public interface IWidget : IInputHandler, IGraphicComponent
    {
    }


}