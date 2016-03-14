using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Views
{
    public interface ITableRenderer<T>
    {
        ISelectableGraphicComponent CreateComponent(int row, int column, T data);
    }
}
