using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Views
{
    public class VisibiltyChangeArgs : EventArgs {
        public bool isVisible;
    }
    public interface IWidget : IGraphicComponent, IInputHandler
    {
        event EventHandler<VisibiltyChangeArgs> VisiblityChanged;
        event EventHandler GetFocus;

        bool IsVisible { get; set; }
    }
}
