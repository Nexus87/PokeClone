using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum MenuType
    {
        Main,
        Attack,
        PKMN,
        Item
    }


    public class SelectionEventArgs : EventArgs
    {
        public int NewSelection { get; set; }
    }


    public interface IMenuModel
    {
        event EventHandler<SelectionEventArgs> OnSelectionChanged;
        MenuType Type { get; }
        MenuType Select();
        MenuType Back();
        void HandleDirection(Direction direction);
    }
}
