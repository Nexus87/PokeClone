using BattleLib.Components;
using Microsoft.Xna.Framework;
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

    }
}
