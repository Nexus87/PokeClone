using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Views
{
    public interface IItemModel<T>
    {
        int Rows { get; }
        int Columns { get; }

        T DataAt(int row, int column);
        string DataStringAt(int row, int column);
    }
}
