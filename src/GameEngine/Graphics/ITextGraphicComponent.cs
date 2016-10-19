namespace GameEngine.Graphics
{
    public interface ITextGraphicComponent : IGraphicComponent
    {
        /// <summary>
        /// Returns the number of characters, that can be displayed
        /// </summary>
        /// <returns></returns>
        int DisplayableChars();

        /// <summary>
        /// This property holds the preferred text size.
        /// If the Height property allows it, this the drawn text will have this size.
        /// </summary>
        float PreferredTextHeight { get; set; }

        /// <summary>
        /// Returns the real text height. This is the real size, that the drawn text will have.
        /// It will always be less than the components Height.
        /// The concrete value depends on the implementing class.
        /// </summary>
        float RealTextHeight { get; }

        /// <summary>
        /// This property holds the text that should be drawn.
        /// How the text is displayed, depends on the implementing class.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// SpriteFont used to draw the string.
        /// </summary>
        ISpriteFont SpriteFont { get; set; }
    }
}
