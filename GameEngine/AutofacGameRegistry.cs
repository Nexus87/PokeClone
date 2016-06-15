using Autofac;
using Autofac.Builder;
using Autofac.Core;
using GameEngine.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameEngine
{
    internal class AutofacGameRegistry : IGameRegistry
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

            return container.Resolve<T>();
        }

        public T ResolveTypeWithParameters<T>(IDictionary<Type, object> parameters)
        {
            if (container == null)
                container = builder.Build();

            var parameterList = from p in parameters select new TypedParameter(p.Key, p.Value);
            return container.Resolve<T>(parameterList);
        }

        public void RegisterType<T>(Func<IGameRegistry, T> creatorFunc)
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

        public void RegisterTypeAs<T, S>(Func<IGameRegistry, T> creatorFunc)
        {
            builder.Register(c => creatorFunc(this)).As<S>();
        }

        public void ScanAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes().
                Where(t => Attribute.IsDefined(t, typeof(GameComponentAttribute)));

            foreach (var t in types)
                RegistertTypeWithAttribute(t);
        }

        private void RegistertTypeWithAttribute(Type t)
        {
            var attribute = t.GetCustomAttribute<GameComponentAttribute>();

            if (t.IsGenericType)
                SetAdditionalCondition(builder.RegisterGeneric(t), attribute);
            else
                SetAdditionalCondition(builder.RegisterType(t), attribute);

            container = null;
        }

        private void SetAdditionalCondition<T, T2, T3>(IRegistrationBuilder<T, T2, T3> registrationBuilder, GameComponentAttribute attribute)
        {
            if (attribute.RegisterType != null)
                registrationBuilder = registrationBuilder.As(attribute.RegisterType);
            if (attribute.SingleInstance)
                registrationBuilder.SingleInstance();
        }
    }
}