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
        void RegisterGameComponentType<T>(IDictionary<Type, object> typedParameters = null, IDictionary<String, object> namedParameters = null) 
            where T : GameEngine.IGameComponent;
        void RegisterGameComponentAsType<T, S>(IDictionary<Type, object> typedParameters = null, IDictionary<String, object> namedParameters = null) where T : GameEngine.IGameComponent;
        void RegisterGraphicComponentType<T>(IDictionary<Type, object> typedParameters = null, IDictionary<String, object> namedParameters = null) where T : IGraphicComponent;
        void RegisterGraphicComponentAsType<T, S>(IDictionary<Type, object> typedParameters = null, IDictionary<String, object> namedParameters = null) where T : IGraphicComponent;
        void RegisterGraphicComponentAsType(Type T, Type S, IDictionary<Type, object> typedParameters = null, IDictionary<String, object> namedParameters = null);
        void RegisterTypeAs<T, S>(IDictionary<Type, object> typedParameters = null, IDictionary<String, object> namedParameters = null);

        IEnumerable<IGameComponent> CreateGameComponents();
        T ResolveType<T>();
        T ResolveTypeWithParameters<T>(IDictionary<Type, object> parameters);

    }
}
