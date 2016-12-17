using System;

namespace GameEngine.GUI.Graphics.TableView
{
    public interface ITableView<T> : IGraphicComponent
    {
        /// <summary>
        /// This event is issued, if the Columns or Rows Property was changed
        /// </summary>
        event EventHandler<TableResizeEventArgs> TableResized;

        /// <summary>
        /// This event is called, if the selection changed
        /// </summary>
        event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        /// <summary>
        /// The total number of columns available
        /// </summary>
        int Columns { get; }
        /// <summary>
        /// The total number of rows available
        /// </summary>
        int Rows { get; }

        /// <summary>
        /// This index determines at which cell the table view start.
        /// This means, that the cell with this index is at the top left of the view.
        /// The StartIndex has to be between (0, 0) and the EndIndex. Both limits are included.
        /// StartIndex = null is equal to (0, 0).
        /// <remarks>
        /// If Row or Columns change, this property must adapt itself to the new size, to
        /// adhere to the above constraints.
        /// </remarks>
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is not between the above bounds.</exception>
        TableIndex? StartIndex { get; set; }
        /// <summary>
        /// This index determines at which cell the table view ends.
        /// This means, that the cell with this index is at the bottom right of the view.
        /// The EndIndex has to be between StartIndex and (Rows - 1, Columns - 1). Both limits are included.
        /// EndIndex = null is equal to (Rows - 1, Columns - 1).
        /// </summary>
        /// <remarks>
        /// If Row or Columns change, this property must adapt itself to the new size, to
        /// adhere to the above constraints.
        /// </remarks>
        /// <exception cref="System.ArgumentOutOfRangeException">If the value is not between the above bounds.</exception>
        TableIndex? EndIndex { get; set; }


        /// <summary>
        /// Set the selection of the cell at that position
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        /// <param name="isSelected">Cell selection</param>
        /// <returns>True if the selection was changed</returns>
        bool SetCellSelection(int row, int column, bool isSelected);

        ITableModel<T> Model { get; }
    }
}