using GameEngine;
using GameEngine.Graphics;
using GameEngineTest.Graphics;
using Microsoft.Xna.Framework;
using System;
using GameEngine.Graphics.General;
using GameEngine.Graphics.TableView;

namespace GameEngineTest.TestUtils
{
    public class TableViewMock : ITableView<TestType>
    {
        public virtual event EventHandler<TableResizeEventArgs> TableResized;
        public event EventHandler<GraphicComponentSizeChangedEventArgs> PreferredSizeChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        public virtual event EventHandler<SelectionChangedEventArgs> SelectionChanged { add { } remove { } }

        public bool SelectionReturnValue { get; set; }

        public TableViewMock()
        {
            SelectionReturnValue = true;
        }
        public void RaiseTableResizeEvent(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            if (StartIndex != null)
            {
                var startIdx = StartIndex.Value;
                var newStartIdx = new TableIndex(startIdx.Row, startIdx.Column);

                // If the table should shrink, we may have to adjust the indexes
                if (startIdx.Row >= rows)
                    newStartIdx.Row = rows - 1;
                if (startIdx.Column >= columns)
                    newStartIdx.Column = columns - 1;

                StartIndex = newStartIdx;
            }

            if (EndIndex != null)
            {
                var endIdx = EndIndex.Value;
                var newEndIdx = new TableIndex(endIdx.Row, endIdx.Column);

                // If the table should shrink, we may have to adjust the indexes
                if (endIdx.Row >= rows)
                    newEndIdx.Row = rows - 1;
                if (endIdx.Column >= columns)
                    newEndIdx.Column = columns - 1;

                EndIndex = newEndIdx;
            }
            
            if (TableResized != null)
                TableResized(this, new TableResizeEventArgs(rows, columns));
        }

        public int Columns { get; set; }
        public int Rows { get; set; }

        private TableIndex? startIndex;
        private TableIndex? endIndex;
        public TableIndex? StartIndex
        {
            get { return startIndex; }
            set
            {
                if (value == null)
                {
                    startIndex = value;
                    return;
                }

                var idx = value.Value;
                
                if ((idx.Row >= Rows || idx.Column >= Columns) && idx.Row != 0 && idx.Column != 0)
                    throw new ArgumentOutOfRangeException();

                startIndex = idx;
            }
        }

        public TableIndex? EndIndex
        {
            get { return endIndex; }
            set
            {
                if (value == null)
                {
                    endIndex = value;
                    return;
                }

                var idx = value.Value;
                if (idx.Row > Rows || idx.Column > Columns)
                    throw new ArgumentOutOfRangeException();

                endIndex = idx;
            }
        }

        public virtual bool SetCellSelection(int row, int column, bool isSelected)
        {
            return SelectionReturnValue;
        }

        public virtual ITableModel<TestType> Model { get; set; }

        public IEngineInterface Game
        {
            get { throw new NotImplementedException(); }
        }

        public void PlayAnimation(IAnimation animation)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<GraphicComponentSizeChangedEventArgs> SizeChanged { add { } remove { } }

        public float XPosition { get; set; }
        public float YPosition { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public void Draw(GameTime time, ISpriteBatch batch)
        {
        }

        public virtual void Setup()
        {
        }


        public Color Color { get; set; }

        public float PreferredHeight
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public float PreferredWidth
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public ResizePolicy HorizontalPolicy
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public ResizePolicy VerticalPolicy
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        public bool IsVisible
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
