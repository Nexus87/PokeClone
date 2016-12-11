using System;
using System.Reflection;

namespace GameEngine.Registry
{
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

        T ResolveType<T>();
        void ScanAssembly(Assembly assembly);
    }
}
