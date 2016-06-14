using GameEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public interface IGameRegistry
    {
        void RegisterGameComponentType<T>() where T : GameEngine.IGameComponent;
        void RegisterGameComponentAsType<T, S>() where T : GameEngine.IGameComponent;
        void RegisterGraphicComponentType<T>() where T : IGraphicComponent;
        void RegisterGraphicComponentAsType<T, S>() where T : IGraphicComponent;
        void RegisterGraphicComponentAsType(Type T, Type S);
        void RegisterTypeAs<T, S>();

        IEnumerable<IGameComponent> CreateGameComponents();
        T ResolveType<T>();
        T ResolveTypeWithParameters<T>(IDictionary<Type, object> parameters);

    }
}
