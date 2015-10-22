using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components
{
    public abstract class AbstractListModel<T> : AbstractMenuModel<T>
    {
        int NewSelection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return selectedIndex > 0 ? selectedIndex - 1 : selectedIndex;
                case Direction.Down:
                    return selectedIndex < items.Count() - 1 ? selectedIndex + 1 : selectedIndex;
            }

            return selectedIndex;
        }

        public override void HandleDirection(Direction direction)
        {
            int newIndex = NewSelection(direction);
            UpdateIndex(newIndex);
        }
    }
}
