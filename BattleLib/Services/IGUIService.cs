using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.GraphicComponents
{
    public interface IGUIService
    {
        event EventHandler TextDisplayed;
        event EventHandler MenuShowed;

        void ShowMenu();
        void SetText(string Text);
    }
}
