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

    public enum MenuType
    {
        Main,
        PKMN,
        Item
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class SelectionEventArgs : EventArgs
    {
        public int NewSelection { get; set; }
    }
      

    public class MenuModel
    {
        public event EventHandler OnMenuChanged;
        public event EventHandler<SelectionEventArgs> OnSelectionChanged;

        int selectedItem;
        public List<String> TextItems { get; set; }

        readonly List<String> mainMenu = new List<String> { "Attack", "PKMN", "Items", "Run" };
        IDirectionHandler handler;

        public MenuOrdering MenuOrdering { get; private set; }
        public MenuType MenuType { get; private set; }

        public MenuModel()
        {
            TextItems = mainMenu;
            selectedItem = 0;
            handler = new TableHandler();
        }

        public void HandleDirection(Direction direction)
        {
            int newSelection = handler.HandleDirection(selectedItem, direction);
            if (newSelection == selectedItem)
                return;

            selectedItem = newSelection;
            if(OnSelectionChanged != null)
                OnSelectionChanged(this, new SelectionEventArgs { NewSelection = newSelection });
        }

        private interface IDirectionHandler
        {
            int HandleDirection(int currentSelected, Direction direction);
        }

        private class ListHandler : IDirectionHandler
        {
            int max;
            public ListHandler(int max)
            {
                this.max = max;
            }
            public int HandleDirection(int currentSelected, Direction direction)
            {
                switch (direction)
                {
                    case Direction.Left:
                    case Direction.Right:
                        return currentSelected;
                    case Direction.Up:
                        return currentSelected == 0 ? currentSelected : currentSelected - 1;
                    case Direction.Down:
                        return currentSelected == max ? currentSelected : currentSelected + 1;
                }

                // Should never happen
                return 0;
            }
        }

        // Assume this to be a 2x2 table
        private class TableHandler : IDirectionHandler
        {
            public int HandleDirection(int currentSelected, Direction direction)
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
}
