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

        public MenuComponent()
        {
            currentState = new NullMenuModel();
            AddModel(currentState);
            
        }

        public void SetMenu(MenuType type)
        {
            ChangeMenu(type);
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

            IMenuModel newState;
            if (!models.TryGetValue(type, out newState))
                throw new InvalidOperationException("Menu type \"" + type + "\" not found");
            
            currentState.Clean();
            currentState = newState;
            currentState.Init();

            if (OnMenuChanged != null)
                OnMenuChanged(this, new MenuChangedArgs { MenuType = type });
        }
    }
}
