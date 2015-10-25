using BattleLib.Components.Menu;
using System.Linq;

namespace BattleLib.Components.Input
{
    public abstract class AbstractListController<T> : AbstractMenuController
        where T:IMenuModel
    {
        protected T model;

        protected AbstractListController(T model)
        {
            this.model = model;
        }

        int NewSelection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return selectedIndex > 0 ? selectedIndex - 1 : selectedIndex;
                case Direction.Down:
                    return selectedIndex < model.Count() - 1 ? selectedIndex + 1 : selectedIndex;
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
