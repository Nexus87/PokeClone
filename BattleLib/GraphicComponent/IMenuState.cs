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
    public interface IMenuState
    {
        int SelectedIndex { set; }

        void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeigth);
    }


}
