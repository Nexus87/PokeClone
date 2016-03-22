using GameEngine.Graphics;
using GameEngine.Graphics.Views;
using GameEngine.Wrapper;
using GameEngineTest.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Util
{
    public class TableViewMock : ITableView<TestType>
    {
        public virtual event EventHandler<TableResizeEventArgs> OnTableResize;
        public virtual event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        public void RaiseTableResizeEvent(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            var endIdx = EndIndex.Value;
            var startIdx = StartIndex.Value;

            var newEndIdx = new TableIndex(endIdx.Row, endIdx.Column);
            var newStartIdx = new TableIndex(startIdx.Row, startIdx.Column);

            // If the table should shrink, we may have to adjust the indexes
            if (endIdx.Row >= rows)
                newEndIdx.Row = rows - 1;
            if (endIdx.Column >= columns)
                newEndIdx.Column = columns - 1;
            if (startIdx.Row >= rows)
                newStartIdx.Row = rows - 1;
            if (startIdx.Column >= columns)
                newStartIdx.Column = columns - 1;

            StartIndex = newStartIdx;
            EndIndex = newEndIdx;
            
            if (OnTableResize != null)
                OnTableResize(this, new TableResizeEventArgs(rows, columns));
        }

        public int Columns { get; set; }
        public int Rows { get; set; }

        public TableIndex? StartIndex { get; set; }
        public TableIndex? EndIndex { get; set; }

        public virtual void SetCellSelection(int row, int column, bool isSelected)
        {
        }

        public virtual ITableModel<TestType> Model { get; set; }

        public GameEngine.PokeEngine Game
        {
            get { throw new NotImplementedException(); }
        }

        public void PlayAnimation(GameEngine.Graphics.Basic.IAnimation animation)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<GraphicComponentSizeChangedEventArgs> SizeChanged;
        public event EventHandler<GraphicComponentPositionChangedEventArgs> PositionChanged;

        public float XPosition { get; set; }
        public float YPosition { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public void Draw(GameTime time, ISpriteBatch batch)
        {
        }

        public virtual void Setup(ContentManager content)
        {
        }
    }
}
