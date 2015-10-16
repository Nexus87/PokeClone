using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components
{ 
    public class MenuChangedArgs : EventArgs
    {
        public MenuType MenuType { get; set; }
    }

    public class MenuComponent 
    {
        public event EventHandler<MenuChangedArgs> OnMenuChanged;

        Dictionary<MenuType, IMenuModel> models = new Dictionary<MenuType, IMenuModel>();
        IMenuModel currentState;

        public void SetMenu(MenuType type)
        {
            if (!models.TryGetValue(type, out currentState))
                throw new InvalidOperationException("Menu type \"" + type + "\" not found");
        }
        public void HandleDirection(Direction direction)
        {
            currentState.HandleDirection(direction);
        }

        public void AddModel(IMenuModel model)
        {
            models.Add(model.Type, model);
        }

        public void Select()
        {
            MenuType type = currentState.Select();
            ChangeMenu(type);
        }

        public void Back()
        {
            MenuType type = currentState.Back();
            ChangeMenu(type);
        }

        private void ChangeMenu(MenuType type)
        {
            if (type == currentState.Type)
                return;

            if (!models.TryGetValue(type, out currentState))
                throw new InvalidOperationException("Menu type \"" + type + "\" not found");

            if (OnMenuChanged != null)
                OnMenuChanged(this, new MenuChangedArgs { MenuType = type });
        }
    }
}
