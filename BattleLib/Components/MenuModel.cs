using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components
{
    public enum MenuOrdering
    {
        Table,
        List
    }

    public class MainMenuModel : IMenuModel
    {
        public event EventHandler<SelectionEventArgs> OnSelectionChanged;

        int selectedItem;
        public List<String> TextItems { get; private set; }

        readonly List<String> mainMenu = new List<String> {
            MenuType.Attack.ToString(),
            MenuType.PKMN.ToString(),
            MenuType.Item.ToString(),
            "Run"
        };

        public MenuOrdering MenuOrdering { get; private set; }
        public MenuType Type { get; private set; }

        public MainMenuModel()
        {
            TextItems = mainMenu;
            Type = MenuType.Main;
            selectedItem = 0;
        }

        public void HandleDirection(Direction direction)
        {
            int newSelection = NewSelection(selectedItem, direction);
            if (newSelection == selectedItem)
                return;

            selectedItem = newSelection;
            if(OnSelectionChanged != null)
                OnSelectionChanged(this, new SelectionEventArgs { NewSelection = newSelection });
        }

        public MenuType Select()
        {
            switch (selectedItem)
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

        public MenuType Back()
        {
            //Main Menu, can't go back
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
