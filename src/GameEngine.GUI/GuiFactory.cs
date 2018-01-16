using System;
using Autofac;
using GameEngine.GUI.Loader;

namespace GameEngine.GUI
{
    public class GuiFactory
    {
        private readonly GuiLoader _loader;
        private readonly IContainer _container;

        public GuiFactory(GuiLoader loader, IContainer container)
        {
            _loader = loader;
            _container = container;
        }

        public T CreateComponent<T>() => _container.Resolve<T>();
        public IGuiComponent LoadFromFile(string path, object controller) => _loader.Load(path, controller);
    }
}