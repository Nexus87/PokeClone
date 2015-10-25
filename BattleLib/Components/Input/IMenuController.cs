using Base;
using BattleLib.Components.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.Input
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class SelectionEventArgs : EventArgs
    {
        public int NewSelection { get; set; }
    }

    public class ItemSelectedEventArgs : EventArgs
    {
        public Item SelectedItem { get; set; }
    }

    public class MoveSelectedEventArgs : EventArgs
    {
        public Move SelectedMove { get; set; }
    }

    public class PKMNSelectedEventArgs : EventArgs
    {
        public Pokemon SelectedPKMN { get; set; }
    }

    public interface IMenuController
    {
        event EventHandler<SelectionEventArgs> OnSelectionChanged;

        event EventHandler<ItemSelectedEventArgs> OnItemSelection;
        event EventHandler<MoveSelectedEventArgs> OnMoveSelected;
        event EventHandler<PKMNSelectedEventArgs> OnPKMNSelected;

        void Setup();
        void TearDown();

        MenuType Select();
        MenuType Back();
        void HandleDirection(Direction direction);
        MenuType Type { get; }

    }
}
