using System;

namespace GameEngine.Graphics
{
    public interface ITableModel<T>
    {
        /// <summary>
        /// Event is raised if the data inside this model changes
        /// </summary>
        event EventHandler<DataChangedEventArgs<T>> DataChanged;
        /// <summary>
        /// Event is raised, if either Rows or Columns property has changed
        /// </summary>
        event EventHandler<TableResizeEventArgs> SizeChanged;
        /// <summary>
        /// The current number of rows of this model
        /// </summary>
        int Rows { get; }
        /// <summary>
        /// The current number of columns of this model
        /// </summary>
        int Columns { get; }

        /// <summary>
        /// Returns the data at the given position.
        /// This function may return null (or default(T)) if no data at
        /// the given position is set.
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        /// <returns>Data at the given index</returns>
        /// <exception cref="System.IndexOutOfRangeException"> 
        /// If row / column &lt; 0  or row >= Rows / column &gt;= columns
        /// </exception>
        T DataAt(int row, int column);
        /// <summary>
        /// Sets the data at the given position. Index
        /// </summary>
        /// <param name="data"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        bool SetDataAt(T data, int row, int column);
    }
}
