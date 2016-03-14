using GameEngine.Graphics.Views;
using GameEngine.Utils;

namespace GameEngineTest.Util
{
    public class TableRendererMock<T> : ITableRenderer<T>
    {
        public T[,] entries = new T[0, 0];

        public GameEngine.Graphics.ISelectableGraphicComponent GetComponent(int row, int column, T data)
        {
            ResizeEntries(row, column);
            entries[row, column] = data;

            return new TestSelectableGraphicComponent();
        }

        private void ResizeEntries(int row, int column)
        {
            if (row >= entries.Rows() || column >= entries.Columns())
            {
                var tmp = new T[row + 1, column + 1];
                entries.Copy(tmp);
                entries = tmp;
            }
        }

        public void Reset()
        {
            entries = new T[0, 0];
        }


        public GameEngine.Graphics.ISelectableGraphicComponent GetComponent(int row, int column)
        {
            ResizeEntries(row, column);

            return new TestSelectableGraphicComponent();
        }

        public GameEngine.Graphics.ISelectableGraphicComponent this[int row, int column]
        {
            get { return GetComponent(row, column); }
        }
    }
}