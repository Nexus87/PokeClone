using System.Collections.Generic;

namespace GameEngine.Tools
{
    public interface IStorage<out T> : IEnumerable<T>
    {
        
    }
}