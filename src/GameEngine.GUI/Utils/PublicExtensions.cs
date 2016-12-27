using System;

namespace GameEngine.GUI.Utils
{
    public static class PublicExtensions
    {
        public static void CheckNull(this Object obj, string variableName)
        {
            if (obj == null)
                throw new ArgumentNullException(variableName + " must not be null");
        }
    }
}