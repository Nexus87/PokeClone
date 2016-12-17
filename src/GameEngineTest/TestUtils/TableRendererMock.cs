using GameEngine.Utils;
using System;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.TableView;

namespace GameEngineTest.TestUtils
{
    public class TableRendererMock<T> : ITableRenderer<T>
    {
        public T[,] Entries = new T[0, 0];
        public TableComponentMock<T>[,] Components = new TableComponentMock<T>[0,0];

        public IGraphicComponent GetComponent(int row, int column, T data)
        {
            Entries = Resize(row, column, Entries);
            Components = Resize(row, column, Components);

            Entries[row, column] = data;

            if (Components[row, column] == null)
                Components[row, column] = new TableComponentMock<T> { Row = row, Column = column, WasDrawn = false };

            var component = Components[row, column];
            component.Data = data;

            return component;
        }

        public void ClearDrawnComponents()
        {
            foreach (var c in Components)
            {
                if (c != null)
                    c.WasDrawn = false;
            }
        }

        private static TS[,] Resize<TS>(int row, int column, TS[,] source)
        {
            if (row >= source.Rows() || column >= source.Columns())
            {
                // Always grow
                var newRows = Math.Max(source.Rows(), row + 1);
                var newColumns = Math.Max(source.Columns(), column + 1);

                var tmp = new TS[newRows, newColumns];
                source.Copy(tmp);
                return tmp;
            }
            else
                return source;
        }

        public void Reset()
        {
            Entries = new T[0, 0];
            Components = new TableComponentMock<T>[0, 0];
        }
    }
}