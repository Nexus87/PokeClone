using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleLib.Components.Menu;

namespace BattleLib.Components.Input
{
    internal class NullController : IMenuController
    {
        public MenuType Type{ get { return MenuType.None; } }

        public event EventHandler<ItemSelectedEventArgs> OnItemSelection;
        public event EventHandler<MoveSelectedEventArgs> OnMoveSelected;
        public event EventHandler<PKMNSelectedEventArgs> OnPKMNSelected;
        public event EventHandler<SelectionEventArgs> OnSelectionChanged;

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
