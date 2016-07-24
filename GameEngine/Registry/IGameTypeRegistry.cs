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
        void RegisterType<T>(Func<IGameTypeRegistry, T> creatorFunc);
        void RegisterTypeAs<T, S>(Func<IGameTypeRegistry, T> creatorFunc);
        void RegisterType<T>();
        void RegisterTypeAs<T, S>();
        void RegisterType(Type t);
        void RegisterTypeAs(Type t, Type s);
        void RegisterAsService<T, S>();
        void RegisterAsService<T, S>(Func<IGameTypeRegistry, T> creatorFunc);
        void RegisterGameComponentForModule<T>(string moduleName) where T : GameEngine.IGameComponent;

        IEnumerable<IGameComponent> CreateGameComponents(string moduleName);
        T ResolveType<T>();
        T ResolveTypeWithParameters<T>(IDictionary<Type, object> parameters);


        void RegisterParameter(Object parameterKey, Object parameter);

        T GetParameter<T>(Object resourceKey);
        void ScanAssembly(Assembly assembly);
    }
}
