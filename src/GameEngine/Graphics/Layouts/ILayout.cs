
namespace GameEngine.Graphics.Layouts
{
    /// <summary>
    /// A class which is responsible for ordering the components of a container
    /// </summary>
    /// <remarks>
    /// For ordering the components, the LayoutContainer member must be called manually.
    /// The layout is not linked to the container in any way. If the size, position or
    /// content of a container changes, the LayoutContainer must be called again in 
    /// order to reorder the components.
    /// </remarks>
    public interface ILayout
    {
        /// <summary>
        /// This function will order the components inside the container.
        /// </summary>
        /// <remarks>
        /// After ordering the components shall still be inside the constraints
        /// of the container.
        /// </remarks>
        /// <param name="container">Container that should be ordered.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when container == null</exception>
        void LayoutContainer(Container container);

        /// <summary>
        /// Set the margin that is left between the border of the container and its components
        /// </summary>
        /// <remarks>
        /// If the margins are set, so that there is no space for the components left,
        /// than the components will have size 0.
        /// This means, if left + right > container.Width => component.Width = 0
        /// </remarks>
        /// <param name="left">Left margin</param>
        /// <param name="right">Right margin</param>
        /// <param name="top">Top margin</param>
        /// <param name="bottom">Bottom margin</param>
        void SetMargin(int left = 0, int right = 0, int top = 0, int bottom = 0);
    }
}
