using System;
using Autofac;
using Autofac.Core.Registration;

namespace GameEngine.TypeRegistry
{
    public class AutofacGameTypeRegistry
    {
        internal IContainer Container { get; set; }


        public T ResolveType<T>()
        {
            try
            {
                return Container.Resolve<T>();
            }
            catch (ComponentNotRegisteredException e)
            {
                throw new TypeNotRegisteredException("Type not found", e);
            }
        }

        public object ResolveGenericType(Type baseType, params Type[] genericTypes)
        {
            var type = baseType.MakeGenericType(genericTypes);
            return Container.Resolve(type);
        }

        
    }
}