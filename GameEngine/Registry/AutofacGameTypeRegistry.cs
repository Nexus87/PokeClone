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

        public void ScanAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes().
                Where(t => Attribute.IsDefined(t, typeof(GameTypeAttribute), false));

            foreach (var t in types)
                RegistertTypeWithAttribute(t);
        }

        private void RegistertTypeWithAttribute(Type t)
        {
            if (t.IsGenericType)
                SetAdditionalCondition(t, builder.RegisterGeneric(t));
            else
                SetAdditionalCondition(t, builder.RegisterType(t));

            container = null;
        }

        private void SetAdditionalCondition<T, T2, T3>(Type t, IRegistrationBuilder<T, T2, T3> registrationBuilder)
            where T2 : ReflectionActivatorData
        {
            var attribute = t.GetCustomAttribute<GameTypeAttribute>();

            if (attribute.RegisterType != null)
                registrationBuilder = registrationBuilder.As(attribute.RegisterType);
            if (attribute.SingleInstance)
                registrationBuilder.SingleInstance();

            if (Attribute.IsDefined(t, typeof(DefaultParameterAttribute), false))
                SetDefaultParameters(t, registrationBuilder);
            if (Attribute.IsDefined(t, typeof(DefaultParameterTypeAttribute), false))
                SetDefaultTypeParameters(t, registrationBuilder);

        }

        private void SetDefaultTypeParameters<T, T2, T3>(Type t, IRegistrationBuilder<T, T2, T3> registrationBuilder)
            where T2 : ReflectionActivatorData
        {
            foreach (var attribute in t.GetCustomAttributes<DefaultParameterTypeAttribute>())
                registrationBuilder.WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.Name.Equals(attribute.ParameterName),
                    (pi, ctx) => ResolveType(attribute.ResolveType)
                    ));
        }

        private object ResolveType(Type t)
        {
            return container.Resolve(t);
        }

        private void SetDefaultParameters<T, T2, T3>(Type t, IRegistrationBuilder<T, T2, T3> registrationBuilder)
            where T2 : ReflectionActivatorData
        {
            foreach (var attribute in t.GetCustomAttributes<DefaultParameterAttribute>())
                registrationBuilder.WithParameter(new ResolvedParameter(
                    (pi, ctx) =>  pi.Name.Equals(attribute.ParameterName), 
                    (pi, ctx) => GetParameter(attribute.ResourceKey)
                    ));
        }

        private object GetParameter(object resourceKey)
        {
            if (!parameters.ContainsKey(resourceKey))
                return null;

            return parameters[resourceKey];
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
    }
}