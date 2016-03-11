using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public interface ISelectable
    {
        bool IsSelected { get; }
        void Select();
        void Unselect();
    }
}
