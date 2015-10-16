using Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components
{
    public class ItemMenuModel : IMenuModel
    {
        int selectedIndex = 0;
        readonly List<Item> items = new List<Item>();

        public ItemMenuModel()
        {
            for (int i = 0; i < 10; i++)
                items.Add(new Item { Name = "Item" + (i + 1) });
        }

        public event EventHandler<SelectionEventArgs> OnSelectionChanged;

        public MenuType Type{ get { return MenuType.Item; } }

        public MenuType Select()
        {
            throw new NotImplementedException();
        }

        public MenuType Back()
        {
            return MenuType.Main;
        }

        int NewSelection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return selectedIndex > 0 ? selectedIndex - 1 : selectedIndex;
                case Direction.Down:
                    return selectedIndex < items.Count - 1 ? selectedIndex + 1 : selectedIndex;
            }

            return selectedIndex;
        }

        public void HandleDirection(Direction direction)
        {
            int newIndex = NewSelection(direction);

            if (newIndex == selectedIndex)
                return;

            selectedIndex = newIndex;
            if (OnSelectionChanged != null)
                OnSelectionChanged(this, new SelectionEventArgs { NewSelection = selectedIndex });
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach(var item in items)
                yield return item.Name;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
