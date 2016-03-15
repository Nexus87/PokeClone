using GameEngine.Graphics.Views;
using GameEngine.Utils;

namespace GameEngineTest.Util
{
    public class TableRendererMock<T> : ITableRenderer<T>
    {
        public T[,] entries = new T[0, 0];
        public bool[,] selections = new bool[0,0];

        public GameEngine.Graphics.ISelectableGraphicComponent GetComponent(int row, int column, T data, bool isSelected)
        {
            entries = Resize(row, column, entries);
            selections = Resize(row, column, selections);

            entries[row, column] = data;
            selections[row, column] = isSelected;

            return new TestSelectableGraphicComponent();
        }

        private T[,] Resize<T>(int row, int column, T[,] source)
        {
            if (row >= source.Rows() || column >= source.Columns())
            {
                var tmp = new T[row + 1, column + 1];
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
        }
    }
}