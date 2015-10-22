using BattleLib.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.GraphicComponent
{
    public class MenuGraphics
    {
        Dictionary<MenuType, IMenuState> menus = new Dictionary<MenuType, IMenuState>();
        IMenuState currentState;

        public MenuGraphics()
        {
            Add(MenuType.None, new NullMenuState());
        }

        public void OnMenuChange(Object source, MenuChangedArgs args)
        {
            if(currentState != null)
                currentState.OnHide();
            currentState = menus[args.MenuType];
            currentState.OnShow();
        }

        public void Add(MenuType type, IMenuState state)
        {
            menus.Add(type, state);
        }

        public void SetMenu(MenuType type)
        {
            if (!menus.TryGetValue(type, out currentState))
                throw new InvalidOperationException("State \"" + type + "\" not found");
        }

        public void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeight)
        {
            currentState.Draw(time, batch, screenWidth, screenHeight);
        }

        public void Setup(ContentManager content)
        {
            foreach (var item in menus.Values)
                item.Setup(content);
            if (currentState == null)
                currentState = menus.First().Value;
        }

    }
}
