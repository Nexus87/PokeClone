using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Views
{
    public interface ITableRenderer<T>
    {
        ISelectableGraphicComponent GetComponent(int row, int column, T data);
    }
}
