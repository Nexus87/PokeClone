namespace GameEngine.GUI.Graphics
{
    /// <summary>
    /// This interface represents a ITextGraphicComponent, that is able to
    /// split the given text into multiple line.
    /// </summary>
    public interface ITextGraphicContainer : IGraphicComponent
    {
        /// <summary>
        /// Checks whether this component has another line, that is
        /// not yet displayed.
        /// </summary>
        /// <returns>True if it has another line</returns>
        bool HasNext();
        /// <summary>
        /// Displays the next 
        /// </summary>
        void NextLine();

        /// <summary>
        /// Text to be displayed
        /// </summary>
        string Text { get; set; }
    }
}
