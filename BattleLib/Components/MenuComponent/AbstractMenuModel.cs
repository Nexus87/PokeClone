using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components
{
    public abstract class AbstractMenuModel<T> : IMenuModel
    {
        protected int selectedIndex = 0;
        protected IEnumerable<T> items;
        public virtual void Init() { }
        public virtual void Clean() { }

        public event EventHandler<SelectionEventArgs> OnSelectionChanged;

        public abstract MenuType Type { get; }
        public abstract MenuType Select();
        public abstract MenuType Back();

        public abstract void HandleDirection(Direction direction);

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var item in items)
                yield return item.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

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
    }
}
