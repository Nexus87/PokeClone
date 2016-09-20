using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameEngine.Registry
{
    internal class AutofacGameTypeRegistry : IGameTypeRegistry
    {
        private IContainer container;
        private readonly ContainerBuilder builder = new ContainerBuilder();

        public AutofacGameTypeRegistry()
        {
            builder.Register<IGameTypeRegistry>(p => this);
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

            try
            {
                return container.Resolve<T>();
            }
            catch (ComponentNotRegisteredException e)
            {
                throw new TypeNotRegisteredException("Type not found", e);
            }
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

        public void ScanAssembly(Assembly assembly)
        {
            RegisterGameTypes(assembly);
            RegisterServiceTypes(assembly);
        }

        private void RegisterGameTypes(Assembly assembly)
        {
            var typesToRegister = assembly.GetTypes()
                .Where(type => Attribute.IsDefined(type, typeof(GameTypeAttribute)));

            foreach (var type in typesToRegister)
            {
                if (type.IsGenericType)
                    builder.RegisterGeneric(type);
                else
                    builder.RegisterType(type);
            }

        }


        private void RegisterServiceTypes(Assembly assembly)
        {
            var typesToRegister = assembly.GetTypes()
                .Where(type => Attribute.IsDefined(type, typeof(GameServiceAttribute)));

            foreach (var type in typesToRegister)
                if(type.IsGenericType)
                    builder.RegisterGeneric(type)
                        .As(type.GetCustomAttribute<GameServiceAttribute>().ServiceType)
                        .SingleInstance();
                else
                    builder.RegisterType(type)
                        .As(type.GetCustomAttribute<GameServiceAttribute>().ServiceType)
                        .SingleInstance();
        }
    }
}