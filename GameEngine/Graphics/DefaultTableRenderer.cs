using GameEngine.Utils;
using System;

namespace GameEngine.Graphics
{
    public class DefaultTableRenderer<T> : ITableRenderer<T>
    {
        ISelectableTextComponent[,] boxes = new ISelectableTextComponent[1, 1];

        public string DefaultString { get; set; }


        public DefaultTableRenderer(GraphicComponentFactory factory)
        {
            this.factory = factory;
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
            if (row < 0)
                throw new ArgumentOutOfRangeException("row", "row must be positive");
            if(column < 0)
                throw new ArgumentOutOfRangeException("column", "column must be positive");
            if (row >= boxes.Rows() || column >= boxes.Columns())
            {
                var tmp = new ISelectableTextComponent[row + 1, column + 1];
                boxes.Copy(tmp);
                boxes = tmp;
            }

            if (boxes[row, column] == null)
            {
                var box = CreateComponent(); 
                box.Setup();
                boxes[row, column] = box;
            }
        }

        protected virtual ISelectableTextComponent CreateComponent()
        {
            return factory.CreateGraphicComponent<ItemBox>();
        }


        public GraphicComponentFactory factory { get; set; }
    }
}
