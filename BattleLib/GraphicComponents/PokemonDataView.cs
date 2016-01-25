using GameEngine.Graphics;
using GameEngine.Graphics.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.GraphicComponents
{
    class PokemonDataView : AbstractGraphicComponent
    {
        VBoxLayout layout = new VBoxLayout();

        public override void Setup(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            throw new NotImplementedException();
        }

        protected override void DrawComponent(Microsoft.Xna.Framework.GameTime time, GameEngine.Wrapper.ISpriteBatch batch)
        {
            throw new NotImplementedException();
        }
    }
}
