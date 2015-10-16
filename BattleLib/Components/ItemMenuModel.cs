using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components
{
    public class ItemMenuModel : IMenuModel
    {
        int selectedIndex = 0;
        public List<Item> Items { get; private set; }

        public ItemMenuModel()
        {

            Items = new List<Item>();
            for (int i = 0; i < 10; i++)
                Items.Add(new Item { Name = "Item" + (i + 1) });
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
                    return selectedIndex < Items.Count - 1 ? selectedIndex + 1 : selectedIndex;
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
    }
}
