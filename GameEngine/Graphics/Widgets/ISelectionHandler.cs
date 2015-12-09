using GameEngine.Graphics.Views;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public interface ISelectionHandler : IInputHandler
    {
        event EventHandler<EventArgs> SelectionChanged;
        event EventHandler<EventArgs> ItemSelected;

        void Init(IItemView view);
        int SelectedRow{ get; }
        int SelectedColumn { get; }
    }
}
