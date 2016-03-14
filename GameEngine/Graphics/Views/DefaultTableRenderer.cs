using GameEngine.Utils;
using GameEngine.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Views
{
    public class DefaultTableRenderer<T> : ITableRenderer<T>
    {
        PokeEngine game;
        SpriteFontCreator creator;
        ItemBox[,] boxes = new ItemBox[1,1];

        public DefaultTableRenderer(PokeEngine game, SpriteFontCreator creator)
        {
            this.game = game;
            this.creator = creator;
        }

        public ISelectableGraphicComponent GetComponent(int row, int column, T data)
        {
            var ret = GetComponentImpl(row, column);
            ret.Text = data == null ? "" : data.ToString();

            return ret;
        }


        public ISelectableGraphicComponent GetComponent(int row, int column)
        {
            return GetComponentImpl(row, column);
        }

        private ItemBox GetComponentImpl(int row, int column)
        {
            if (row >= boxes.Rows() || column >= boxes.Columns())
            {
                var tmp = new ItemBox[row + 1, column + 1];
                boxes.Copy(tmp);
                boxes = tmp;
            }

            if (boxes[row, column] == null)
            {
                var box = new ItemBox(creator(), game);
                boxes[row, column] = box;
            }

            return boxes[row, column];
        }

        public ISelectableGraphicComponent this[int row, int column]
        {
            get { return GetComponent(row, column); }
        }
    }
}
