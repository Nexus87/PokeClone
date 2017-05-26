using System.Linq;
using Autofac;
using GameEngine.Globals;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;
using GameEngine.GUI.Components;
using GameEngine.GUI.Panels;
using GameEngine.GUI.Renderers;
using GameEngine.TypeRegistry;

namespace GameEngine.Core
{
    internal class GameEngineModule : Module
    {
        private readonly GameRunner _engine;
        private readonly TextureProvider _textureProvider;
        private readonly ScreenConstants _screenConstants;

        public GameEngineModule(GameRunner engine, TextureProvider textureProvider)
        {
            _engine = engine;
            _textureProvider = textureProvider;
            _screenConstants = new ScreenConstants();
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultTextSplitter>().AsImplementedInterfaces();
            builder.Register(x => _engine.Content).AsSelf().SingleInstance();
            builder.Register(x => _textureProvider).AsSelf().SingleInstance();
            builder.Register(x => _engine).AsImplementedInterfaces();
            builder.Register(x => _screenConstants);
            builder.Register(x => _engine.GraphicsDeviceManager);

            builder.RegisterAssemblyTypes(new[]
                {
                    typeof(GameEngineModule).Assembly,
                    typeof(IGuiComponent).Assembly,
                    typeof(TextureBuilder).Assembly
                }).Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(GameServiceAttribute)))
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();

            builder.RegisterAssemblyTypes(new[]
                {
                    typeof(GameEngineModule).Assembly,
                    typeof(IGuiComponent).Assembly,
                    typeof(TextureBuilder).Assembly
                }).Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(GameTypeAttribute)))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerDependency();

            builder.Register(
                x => new Panel(x.Resolve<PanelRenderer>())
                {
                    BackgroundColor = x.Resolve<ScreenConstants>().BackgroundColor
                });
        }
    }
}
