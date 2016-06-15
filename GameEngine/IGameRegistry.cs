using GameEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public interface IGameRegistry
    {
        void ScanAssembly(Assembly assembly);
        void RegisterType<T>(Func<IGameRegistry, T> creatorFunc);
        void RegisterTypeAs<T, S>(Func<IGameRegistry, T> creatorFunc);
        void RegisterType<T>();
        void RegisterTypeAs<T, S>();
        void RegisterGenericTypeAs(Type T, Type S);
        void RegisterGenericType(Type T);

        IEnumerable<IGameComponent> CreateGameComponents();
        T ResolveType<T>();
        T ResolveTypeWithParameters<T>(IDictionary<Type, object> parameters);

    }
}
