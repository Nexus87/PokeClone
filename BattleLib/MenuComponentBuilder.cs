using BattleLib.Components;
using BattleLib.GraphicComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib
{
    public class MenuComponentBuilder
    {
        public MenuComponent Component { get; private set; }
        public MenuGraphics Graphics { get; private set; }

        public MenuComponentBuilder(MenuComponent component, MenuGraphics graphics)
        {
            Component = component;
            Graphics = graphics;
            Component.OnMenuChanged += Graphics.OnMenuChange;
        }

        public MenuComponentBuilder() : this(new MenuComponent(), new MenuGraphics())
        { }

        public void AddMenu(IMenuModel model, IMenuState state)
        {
            Component.AddModel(model);
            Graphics.Add(model.Type, state);
        }
    }
}
