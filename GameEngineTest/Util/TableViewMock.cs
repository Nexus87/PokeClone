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
