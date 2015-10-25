using BattleLib.Components.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.Menu
{ 


    public class MenuComponent 
    {

        Dictionary<MenuType, IMenuModel> models = new Dictionary<MenuType, IMenuModel>();
        IMenuModel currentState;

        public void OnMenuChanged(Object sender, MenuChangedArgs args)
        {
            IMenuModel newState;
            if (!models.TryGetValue(args.MenuType, out newState))
                throw new InvalidOperationException("State '" + args.MenuType + "' not found.");

            currentState = newState;
        }

        public MenuComponent()
        {
            currentState = new NullMenuModel();
            AddModel(currentState);
            
        }

        public void AddModel(IMenuModel model)
        {
            models.Add(model.Type, model);
        }
    }
}
