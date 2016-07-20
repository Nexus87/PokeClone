using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Core.Registration;
using GameEngine.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameEngine.Registry
{
    internal class AutofacGameTypeRegistry : IGameTypeRegistry
    {
        private IContainer container;
        private ContainerBuilder builder = new ContainerBuilder();

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

            try
            {
                return container.Resolve<T>();
            }
            catch (ComponentNotRegisteredException e)
            {
                throw new TypeNotRegisteredException("Type not found", e);
            }
        }

        public T ResolveTypeWithParameters<T>(IDictionary<Type, object> parameters)
        {
            if (container == null)
                container = builder.Build();

            var parameterList = from p in parameters select new TypedParameter(p.Key, p.Value);
            return container.Resolve<T>(parameterList);
        }

        public void RegisterType<T>(Func<IGameTypeRegistry, T> creatorFunc)
        {
            RegisterTypeAs<T, T>(creatorFunc);
        }

        public void RegisterType<T>()
        {
            RegisterTypeAs<T, T>();
        }

        public void RegisterGenericTypeAs(Type T, Type S)
        {
            builder.RegisterGeneric(T).As(S);
            container = null;
        }

        public void RegisterGenericType(Type T)
        {
            RegisterGenericTypeAs(T, T);
        }

        public void RegisterTypeAs<T, S>(Func<IGameTypeRegistry, T> creatorFunc)
        {
            builder.Register(c => creatorFunc(this)).As<S>();
        }


        private readonly Dictionary<object, object> parameters = new Dictionary<object, object>();

        public void RegisterParameter(object parameterKey, object parameter)
        {
            parameters[parameterKey] = parameter;
        }


        public T GetParameter<T>(object resourceKey)
        {
            return (T)parameters[resourceKey];
        }


        public void RegisterAsService<T, S>()
        {
            builder.RegisterType<T>().As<S>().SingleInstance();
            container = null;
        }

        public void RegisterGameComponentForModule<T>(string moduleName) where T : GameEngine.IGameComponent
        {
            builder.RegisterType<T>().
                As<T>().
                SingleInstance().
                Keyed<GameEngine.IGameComponent>(moduleName).
                PreserveExistingDefaults();
        }

        public IEnumerable<IGameComponent> CreateGameComponents(string moduleName)
        {
            if (container == null)
                container = builder.Build();
            return container.ResolveKeyed<IEnumerable<GameEngine.IGameComponent>>(moduleName);
        }


        public void RegisterType(Type t)
        {
            if (t.IsGenericType)
                builder.RegisterGeneric(t);
            else
                builder.RegisterType(t);
        }

        public void RegisterTypeAs(Type t, Type s)
        {
            if (t.IsGenericType)
                builder.RegisterGeneric(t).As(s);
            else
                builder.RegisterType(t).As(s);
        }


        public void RegisterAsService<T, S>(Func<IGameTypeRegistry, T> creatorFunc)
        {
            builder.Register<T>(c => creatorFunc(this)).As<S>().SingleInstance();
        }
    }
}