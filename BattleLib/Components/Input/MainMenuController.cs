using BattleLib.Components.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.Input
{
    public class MainMenuController : AbstractMenuController
    {
        public override MenuType Type { get { return MenuType.Main; } }

        public override MenuType Back()
        {
            return Type;
        }

        public override void HandleDirection(Direction direction)
        {
            int newSelection = NewSelection(selectedIndex, direction);
            UpdateIndex(newSelection);
        }

        public override MenuType Select()
        {
            switch (selectedIndex)
            {
                case 0:
                    return MenuType.Attack;
                case 1:
                    return MenuType.PKMN;
                case 2:
                    return MenuType.Item;
            }

            return Type;
        }

        private int NewSelection(int currentSelected, Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    return currentSelected < 2 ? currentSelected + 2 : currentSelected;
                case Direction.Up:
                    return currentSelected < 2 ? currentSelected : currentSelected - 2;
                case Direction.Left:
                    return currentSelected % 2 == 0 ? currentSelected : currentSelected - 1;
                case Direction.Right:
                    return currentSelected % 2 == 0 ? currentSelected + 1 : currentSelected;
            }

            //Should never happen
            return 0;
        }
    }
}
