using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    internal class AutofacGameRegistry : IGameRegistry
    {
        IContainer container;
        ContainerBuilder builder = new ContainerBuilder();

        public void RegisterGameComponentType<T>() where T : IGameComponent
        {
            RegisterTypeAs<T, T>();
        }

        public void RegisterGameComponentAsType<T, S>() where T : IGameComponent
        {
            RegisterTypeAs<T, S>();
        }

        public void RegisterGraphicComponentType<T>() where T : Graphics.IGraphicComponent
        {
            RegisterTypeAs<T, T>();
        }

        public void RegisterGraphicComponentAsType<T, S>() where T : Graphics.IGraphicComponent
        {
            RegisterTypeAs<T, S>();
        }

        public void RegisterGraphicComponentAsType(Type T, Type S)
        {
            builder.RegisterGeneric(T).As(S);
            container = null;
        }

        public void RegisterTypeAs<T, S>()
        {
            builder.RegisterType<T>().As<S>();
            container = null;
        }

        public IEnumerable<IGameComponent> CreateGameComponents()
        {
            return new List<IGameComponent>();
        }

        public T ResolveType<T>()
        {
            if (container == null)
                container = builder.Build();

            return container.Resolve<T>();
        }

        public T ResolveTypeWithParameters<T>(IDictionary<Type, object> parameters)
        {
            if (container == null)
                container = builder.Build();
            
            var parameterList = from p in parameters select new TypedParameter(p.Key, p.Value);
            return container.Resolve<T>(parameterList);
        }
    }
}
