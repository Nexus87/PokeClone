using System;

namespace GameEngine.TypeRegistry
{
    [Serializable]
    public class TypeNotRegisteredException : Exception {
        public TypeNotRegisteredException(string message, Exception innerException) :
            base(message, innerException)
        { }
    }
}