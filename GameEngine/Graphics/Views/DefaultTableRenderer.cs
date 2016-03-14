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

        public ISelectableGraphicComponent CreateComponent(int row, int column, T data)
        {
            if(row >= boxes.Rows() || column >= boxes.Columns())
            {
                var tmp = new ItemBox[row+1, column+1];
                boxes.Copy(tmp);
                boxes = tmp;
            }

            if(boxes[row, column] == null)
            {
                var box = new ItemBox(creator(), game);
                box.Setup(game.Content);
                boxes[row, column] = box;
            }

            var ret = boxes[row, column];
            ret.Text = data == null ? "" : data.ToString();

            return ret;
        }
    }
}
