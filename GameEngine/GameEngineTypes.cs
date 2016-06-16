using GameEngine.Graphics;
using GameEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    internal static class GameEngineTypes
    {
        public static void Register(IGameRegistry registry, GraphicComponentFactory factory)
        {
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
            registry.RegisterType<ISpriteFont>(r => factory.DefaultFont);
            registry.RegisterType<Line>( r => new Line(factory.Pixel, factory.Cup));

        }
    }
}
