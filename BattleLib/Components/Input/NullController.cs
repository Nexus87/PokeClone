using Base;
using BattleLib.Components.Menu;
using System;

namespace BattleLib.Components.Input
{
    internal class NullController : IMenuController
    {
        public MenuType Type{ get { return MenuType.None; } }

        public event EventHandler<SelectedEventArgs<Item>> OnItemSelection;
        public event EventHandler<SelectedEventArgs<Move>> OnMoveSelected;
        public event EventHandler<SelectedEventArgs<Pokemon>> OnPKMNSelected;
        public event EventHandler<SelectedIndexChangedEvent> OnSelectedIndexChange;

        public MenuType Back()
        {
            return Type;
        }

        public void HandleDirection(Direction direction)
        {
        }

        public MenuType Select()
        {
            return Type;
        }

        public void Setup()
        {
        }

        public void TearDown()
        {
        }
    }
}
