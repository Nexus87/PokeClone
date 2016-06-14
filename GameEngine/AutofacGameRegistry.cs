using Autofac;
using Autofac.Core;
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

        public void RegisterGameComponentType<T>(IDictionary<Type, object> typedParameters = null, IDictionary<String, object> namedParameters = null) where T : IGameComponent
        {
            RegisterTypeAs<T, T>(typedParameters, namedParameters);
        }

        public void RegisterGameComponentAsType<T, S>(IDictionary<Type, object> typedParameters = null, IDictionary<String, object> namedParameters = null) where T : IGameComponent
        {
            RegisterTypeAs<T, S>(typedParameters, namedParameters);
        }

        public void RegisterGraphicComponentType<T>(IDictionary<Type, object> typedParameters = null, IDictionary<String, object> namedParameters = null) where T : Graphics.IGraphicComponent
        {
            RegisterTypeAs<T, T>(typedParameters, namedParameters);
        }

        public void RegisterGraphicComponentAsType<T, S>(IDictionary<Type, object> typedParameters = null, IDictionary<String, object> namedParameters = null) where T : Graphics.IGraphicComponent
        {
            RegisterTypeAs<T, S>(typedParameters, namedParameters);
        }

        public void RegisterGraphicComponentAsType(Type T, Type S, IDictionary<Type, object> typedParameters = null, IDictionary<String, object> namedParameters = null)
        {
            if (typedParameters == null)
                typedParameters = new Dictionary<Type, object>();
            if (namedParameters == null)
                namedParameters = new Dictionary<string, object>();

            var pars = new List<Parameter>(from p in typedParameters select new TypedParameter(p.Key, p.Value));
            pars.AddRange(from p in namedParameters select (Parameter) new NamedParameter(p.Key, p.Value));
            builder.RegisterGeneric(T).As(S).WithParameters(pars);
            container = null;
        }

        public void RegisterTypeAs<T, S>(IDictionary<Type, object> typedParameters = null, IDictionary<String, object> namedParameters = null)
        {
            if (typedParameters == null)
                typedParameters = new Dictionary<Type, object>();
            if (namedParameters == null)
                namedParameters = new Dictionary<string, object>();

            var pars = new List<Parameter>(from p in typedParameters select new TypedParameter(p.Key, p.Value));
            pars.AddRange(from p in namedParameters select (Parameter)new NamedParameter(p.Key, p.Value));
            builder.RegisterType<T>().As<S>().WithParameters(pars);
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
