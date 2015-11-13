using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Views
{
    public interface ISelectionHandler
    {
        event EventHandler<EventArgs> SelectionChanged;
        event EventHandler<EventArgs> ItemSelected;

        Tuple<int, int> SelectedIndex { get; }

        void HandleInput(Keys key);
    }
}
