using GameEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Registry
{
    public class TypeNotRegisteredException : Exception {
        public TypeNotRegisteredException(string message, Exception innerException) :
            base(message, innerException)
        { }
    }

    public interface IGameTypeRegistry
    {
        void ScanAssembly(Assembly assembly);
        void RegisterType<T>(Func<IGameTypeRegistry, T> creatorFunc);
        void RegisterTypeAs<T, S>(Func<IGameTypeRegistry, T> creatorFunc);
        void RegisterType<T>();
        void RegisterTypeAs<T, S>();
        void RegisterGenericTypeAs(Type T, Type S);
        void RegisterGenericType(Type T);

        IEnumerable<IGameComponent> CreateGameComponents();
        T ResolveType<T>();
        T ResolveTypeWithParameters<T>(IDictionary<Type, object> parameters);


        void RegisterParameter(Object parameterKey, Object parameter);

        T GetParameter<T>(Object resourceKey);
    }
}
