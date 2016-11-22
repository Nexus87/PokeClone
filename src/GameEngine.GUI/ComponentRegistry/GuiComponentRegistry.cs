using System.Xml.Linq;
using Autofac;
using Autofac.Core;
using GameEngine.GUI.Builder;

namespace GameEngine.GUI.ComponentRegistry
{
    public class GuiComponentRegistry
    {
        private readonly ContainerBuilder _builder = new ContainerBuilder();
        private IContainer _container;

        public GuiComponentRegistry()
        {
            _builder.Register(t => this);
        }

        public void RegisterSkin<T>() where T : ISkin
        {
            _builder.RegisterType<T>().As<ISkin>();
        }
        public void RegisterBuilderFactory<T>() where T : IBuilderFactory
        {
            _builder.RegisterType<T>().As<IBuilderFactory>();
        }

        public void RegisterGuiComponent<TControll, TControllBuilder>()
            where TControll : IGraphicComponent
            where TControllBuilder : IBuilder
        {
            var name = typeof(TControll).Name;
            _builder.RegisterType<TControllBuilder>().Named<IBuilder>(name);
        }

        public IBuilder GetBuilder(XName elementName)
        {
            return _container.ResolveNamed<IBuilder>(elementName.LocalName);
        }

        public IBuilderFactory GetBuilderFactory()
        {
            return _container.Resolve<IBuilderFactory>();
        }

        public void Init()
        {
            _container =  _builder.Build();
        }
    }
}