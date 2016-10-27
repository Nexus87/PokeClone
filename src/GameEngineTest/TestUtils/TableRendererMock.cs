using GameEngine.Utils;
using System;
using GameEngine.Graphics.TableView;

namespace GameEngineTest.TestUtils
{
    public class TableRendererMock<T> : ITableRenderer<T>
    {
        public T[,] entries = new T[0, 0];
        public bool[,] selections = new bool[0,0];
        public TableComponentMock<T>[,] components = new TableComponentMock<T>[0,0];

        public GameEngine.Graphics.ISelectableGraphicComponent GetComponent(int row, int column, T data, bool isSelected)
        {
            entries = Resize(row, column, entries);
            selections = Resize(row, column, selections);
            components = Resize(row, column, components);

            entries[row, column] = data;
            selections[row, column] = isSelected;

            if (components[row, column] == null)
                components[row, column] = new TableComponentMock<T> { Row = row, Column = column, WasDrawn = false };

            var component = components[row, column];
            component.Data = data;
            component.IsSelected = isSelected;

            return component;
        }

        public void ClearDrawnComponents()
        {
            foreach (var c in components)
            {
                if (c != null)
                    c.WasDrawn = false;
            }
        }

        private S[,] Resize<S>(int row, int column, S[,] source)
        {
            if (row >= source.Rows() || column >= source.Columns())
            {
                // Always grow
                int newRows = Math.Max(source.Rows(), row + 1);
                int newColumns = Math.Max(source.Columns(), column + 1);

                var tmp = new S[newRows, newColumns];
                source.Copy(tmp);
                return tmp;
            }
            else
                return source;
        }

        public void Reset()
        {
            entries = new T[0, 0];
            selections = new bool[0, 0];
            components = new TableComponentMock<T>[0, 0];
        }
    }
}