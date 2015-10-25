using Base;
using BattleLib.Components.Menu;
using System;

namespace BattleLib.Components.Input
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class SelectedIndexChangedEvent : EventArgs
    {
        public int NewSelection { get; set; }
    }

    public class SelectedEventArgs<T> : EventArgs
    {
        public T SelectedValue { get; set; }
    }

    public interface IMenuController
    {
        event EventHandler<SelectedIndexChangedEvent> OnSelectedIndexChange;

        event EventHandler<SelectedEventArgs<Item>> OnItemSelection;
        event EventHandler<SelectedEventArgs<Move>> OnMoveSelected;
        event EventHandler<SelectedEventArgs<Pokemon>> OnPKMNSelected;

        void Setup();
        void TearDown();

        MenuType Select();
        MenuType Back();
        void HandleDirection(Direction direction);
        MenuType Type { get; }

    }
}
