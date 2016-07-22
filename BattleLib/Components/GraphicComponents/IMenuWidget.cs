using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.GraphicComponents
{
    public interface IMenuWidget<T> : IWidget
    {
        event EventHandler ExitRequested;
        event EventHandler<SelectionEventArgs<T>> ItemSelected;

        void ResetSelection();
    }
}
