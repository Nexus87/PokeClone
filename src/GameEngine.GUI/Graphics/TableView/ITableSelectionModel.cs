using System;

namespace GameEngine.GUI.Graphics.TableView
{
    public interface ITableSelectionModel
    {
        event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        /// <summary>
        /// Selects the cell (row, column). Returns true if it was successful
        /// </summary>
        /// <param name="row">Row of the selected cell</param>
        /// <param name="column">Column of the selected cell</param>
        /// <returns>True if successful</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// If row or column is negative. Implementations might have additional
        /// constraints.
        /// </exception>
        bool SelectIndex(int row, int column);

        /// <summary>
        /// Deselects the cell (row, column). Returns true if it was successful
        /// </summary>
        /// <param name="row">Row of the cell</param>
        /// <param name="column">Column of the cell</param>
        /// <returns>True if successful</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// If row or column is negative. Implementations might have additional
        /// constraints.
        /// </exception>
        bool UnselectIndex(int row, int column);

        /// <summary>
        /// Returns true if the cell is selected
        /// </summary>
        /// <param name="row">Row of the cell</param>
        /// <param name="column">Column of the cell</param>
        /// <returns>True if it is currently selected</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// If row or column is negative. Implementations might have additional
        /// constraints.
        /// </exception>
        bool IsSelected(int row, int column);

    }
}
