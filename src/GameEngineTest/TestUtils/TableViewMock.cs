using GameEngine;
using GameEngineTest.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using GameEngine.Globals;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Graphics.TableView;

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

            TableResized?.Invoke(this, new TableResizeEventArgs(rows, columns));
        }

        public int Columns { get; set; }
        public int Rows { get; set; }

        private TableIndex? _startIndex;
        private TableIndex? _endIndex;

        public TableIndex? StartIndex
        {
            get { return _startIndex; }
            set
            {
                if (value == null)
                {
                    _startIndex = null;
                    return;
                }

                var idx = value.Value;

                if ((idx.Row >= Rows || idx.Column >= Columns) && idx.Row != 0 && idx.Column != 0)
                    throw new ArgumentOutOfRangeException();

                _startIndex = idx;
            }
        }

        public TableIndex? EndIndex
        {
            get { return _endIndex; }
            set
            {
                if (value == null)
                {
                    _endIndex = null;
                    return;
                }

                var idx = value.Value;
                if (idx.Row > Rows || idx.Column > Columns)
                    throw new ArgumentOutOfRangeException();

                _endIndex = idx;
            }
        }

        public virtual bool SetCellSelection(int row, int column, bool isSelected)
        {
            return SelectionReturnValue;
        }

        public virtual ITableModel<TestType> Model { get; set; }
        public TableCellFactory<TestType> Factory { get; set; }

        public IEngineInterface Game
        {
            get { throw new NotImplementedException(); }
        }

        public void PlayAnimation(IAnimation animation)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<GraphicComponentSizeChangedEventArgs> SizeChanged { add { } remove { } }

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

        public Rectangle ScissorArea { get; set; }
        Rectangle IGraphicComponent.Area { get; set; }

        public Rectangle Area { get; set; }

        public IGraphicComponent Parent { get; set; }
        public IEnumerable<IGraphicComponent> Children { get; } = new List<IGraphicComponent>();
        public bool IsSelected { get; set; }
        public bool IsSelectable { get; set; }
        public void HandleKeyInput(CommandKeys key)
        {
            throw new NotImplementedException();
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

        public event EventHandler<ComponentSelectedEventArgs> ComponentSelected;
    }
}
