namespace GameEngine.Graphics
{
    /// <summary>
    /// This determines how layouts resize components in a container
    /// </summary>
    public enum ResizePolicy
    {
        /// <summary>
        /// This is the default value. Layouts will resize the components width/height as needed
        /// </summary>
        Extending,
        /// <summary>
        /// The layout will try to resize the component according to its preferred width/height but
        /// also takes care, that the component is inside the constraints of the container.
        /// </summary>
        Preferred,
        /// <summary>
        /// The layout will not resize the component even if its bigger than the surrounding
        /// container.
        /// </summary>
        Fixed
    }
}