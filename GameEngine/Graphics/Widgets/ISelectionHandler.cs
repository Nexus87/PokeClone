using GameEngine.Graphics.Views;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public interface ISelectionHandler<T> : IInputHandler
    {
        event EventHandler<EventArgs> SelectionChanged;
        event EventHandler<EventArgs> ItemSelected;

        void Init(IItemModel<T> model);
        int SelectedRow{ get; }
        int SelectedColumn { get; }
    }
}
