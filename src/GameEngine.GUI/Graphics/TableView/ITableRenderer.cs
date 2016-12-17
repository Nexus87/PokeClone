
namespace GameEngine.GUI.Graphics.TableView
{
    /// <summary>
    /// This class is used, to build the GraphicComponents, used to display the cells in a TableView
    /// </summary>
    /// <typeparam name="T">Type of the data</typeparam>
    public interface ITableRenderer<in T>
    {
        /// <summary>
        /// This function constructs the GraphicComponent for the cell (row, column), with
        /// the given data and selection.
        /// It gets called whenever the data in a model changes or the selection of the 
        /// corresponding cell has changed.
        /// The method is expected to return a fully functional ISelectableGraphicComponent
        /// instance, so that there are no more calls to Setup are necessary.
        /// Therefore, this function should only be called, after the ContentManager is
        /// initialized.
        /// </summary>
        /// <param name="row">Row of the cell</param>
        /// <param name="column">Column of the cell</param>
        /// <param name="data">Data from the model</param>
        /// <param name="isSelected">Cell selection</param>
        /// <returns></returns>
        IGraphicComponent GetComponent(int row, int column, T data, bool isSelected);
    }
}
