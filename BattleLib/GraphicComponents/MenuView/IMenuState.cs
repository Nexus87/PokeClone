using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BattleLib.GraphicComponents.MenuView
{
    public interface IMenuState
    {
        void OnShow();
        void OnHide();
        void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeigth);
        void Setup(ContentManager content);
    }


}
