using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Views
{
    public class DefaultTableRenderer<T> : ITableRenderer<T>
    {
        public ISelectableGraphicComponent GetComponent(int row, int column, T data)
        {
            return new ItemBox()
        }
    }
}
