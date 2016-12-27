using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core.Registration;

namespace GameEngine.TypeRegistry
{
    public class AutofacGameTypeRegistry : IGameTypeRegistry
    {
        private IContainer _container;
        private readonly ContainerBuilder _builder = new ContainerBuilder();

        public AutofacGameTypeRegistry()
        {
            _builder.Register<IGameTypeRegistry>(p => this);
        }
        public void RegisterTypeAs<T, TS>()
        {
            _builder.RegisterType<T>().As<TS>();
            _container = null;
        }

        public T ResolveType<T>()
        {
            if (_container == null)
                _container = _builder.Build();

            try
            {
                return _container.Resolve<T>();
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

        public void RegisterGenericTypeAs(Type T, Type s)
        {
            _builder.RegisterGeneric(T).As(s);
            _container = null;
        }

        public void RegisterGenericType(Type T)
        {
            RegisterGenericTypeAs(T, T);
        }

        public void RegisterTypeAs<T, TS>(Func<IGameTypeRegistry, T> creatorFunc)
        {
            _builder.Register(c => creatorFunc(this)).As<TS>();
        }

        public void RegisterAsService<T, TS>()
        {
            _builder.RegisterType<T>().As<TS>().SingleInstance();
            _container = null;
        }

        public void RegisterType(Type t)
        {
            if (t.IsGenericType)
                _builder.RegisterGeneric(t);
            else
                _builder.RegisterType(t);
        }

        public void RegisterTypeAs(Type t, Type s)
        {
            if (t.IsGenericType)
                _builder.RegisterGeneric(t).As(s);
            else
                _builder.RegisterType(t).As(s);
        }


        public void RegisterAsService<T, TS>(Func<IGameTypeRegistry, T> creatorFunc)
        {
            _builder.Register(c => creatorFunc(this)).As<TS>().SingleInstance();
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
                    _builder.RegisterGeneric(type);
                else
                    _builder.RegisterType(type);
            }

        }


        private void RegisterServiceTypes(Assembly assembly)
        {
            var typesToRegister = assembly.GetTypes()
                .Where(type => Attribute.IsDefined(type, typeof(GameServiceAttribute)));

            foreach (var type in typesToRegister)
                if(type.IsGenericType)
                    _builder.RegisterGeneric(type)
                        .As(type.GetCustomAttribute<GameServiceAttribute>().ServiceType)
                        .SingleInstance();
                else
                    _builder.RegisterType(type)
                        .As(type.GetCustomAttribute<GameServiceAttribute>().ServiceType)
                        .SingleInstance();
        }
    }
}