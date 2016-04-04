using GameEngine.Graphics.Basic;
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
        ISelectableTextComponent[,] boxes = new ISelectableTextComponent[1, 1];

        public string DefaultString { get; set; }


        public DefaultTableRenderer(PokeEngine game)
        {
            this.game = game;
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
                var tmp = new ISelectableTextComponent[row + 1, column + 1];
                boxes.Copy(tmp);
                boxes = tmp;
            }

            if (boxes[row, column] == null)
            {
                var box = CreateComponent(); 
                box.Setup(game.Content);
                boxes[row, column] = box;
            }
        }

        protected virtual ISelectableTextComponent CreateComponent()
        {
            return new ItemBox(new TextureBox("arrow", game), new TextBox("MenuFont", game), game);
        }

    }
}
