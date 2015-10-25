using BattleLib.Components.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.Input
{
    public abstract class AbstractMenuController : IMenuController 
    {
        
        protected int selectedIndex = 0;
        public virtual void Init() { }
        public virtual void Clean() { }

        public event EventHandler<SelectionEventArgs> OnSelectionChanged;

        public virtual event EventHandler<ItemSelectedEventArgs> OnItemSelection;
        public virtual event EventHandler<MoveSelectedEventArgs> OnMoveSelected;
        public virtual event EventHandler<PKMNSelectedEventArgs> OnPKMNSelected;

        public abstract MenuType Type { get; }
        public abstract MenuType Select();

        public virtual MenuType Back() { return MenuType.Main; }

        public abstract void HandleDirection(Direction direction);

        protected void UpdateIndex(int newIndex)
        {
            if (selectedIndex != newIndex)
                SelectIndex(newIndex);
        }
        protected void SelectIndex(int newIndex)
        {
            selectedIndex = newIndex;
            if (OnSelectionChanged != null)
                OnSelectionChanged(this, new SelectionEventArgs { NewSelection = selectedIndex });
        }

        public virtual void Setup()
        {
        }

        public virtual void TearDown()
        {
        }
    }
}
