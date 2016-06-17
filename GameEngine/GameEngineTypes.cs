using GameEngine.Graphics;
using GameEngine.Registry;
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
        public enum ResourceKeys
        {
            PixelTexture,
            CupTexture,
            ArrowTexture,
            BorderTexture
        }

        public static void Register(IGameTypeRegistry registry, GraphicComponentFactory factory)
        {
            registry.RegisterParameter(ResourceKeys.PixelTexture, factory.Pixel);
            registry.RegisterParameter(ResourceKeys.CupTexture, factory.Cup);
            registry.RegisterParameter(ResourceKeys.ArrowTexture, factory.DefaultArrowTexture);
            registry.RegisterParameter(ResourceKeys.BorderTexture, factory.DefaultBorderTexture);

            registry.ScanAssembly(Assembly.GetExecutingAssembly());
            registry.RegisterType<ISpriteFont>(r => factory.DefaultFont);

        }
    }
}
