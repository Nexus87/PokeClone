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
        ISpriteFont font;
        SpriteFontCreator creator;
        ItemBox[,] boxes = new ItemBox[1,1];

        public string DefaultString { get; set; }

        public DefaultTableRenderer(PokeEngine game) :
            this(game, delegate { return new XNASpriteFont("MenuFont", game.Content); })
        { }

        public DefaultTableRenderer(PokeEngine game, SpriteFontCreator creator)
        {
            this.game = game;
            this.creator = creator;
            DefaultString = "";
        }

        public ISelectableGraphicComponent GetComponent(int row, int column, T data, bool IsSelected)
        {
            CheckBounds(row, column);

            var ret = boxes[row, column];
            ret.Text = data == null ? DefaultString : data.ToString();
            
            if (IsSelected)
                ret.Select();
            else
                ret.Unselect();

            return ret;
        }


        private void CheckBounds(int row, int column)
        {
            if (row < 0 || column < 0)
                throw new ArgumentOutOfRangeException("row and column must be positive");

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
        }

    }
}
