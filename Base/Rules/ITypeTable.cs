using System;
namespace Base.Rules
{
    public interface ITypeTable
    {
        float GetModifier(Base.Data.PokemonType source, Base.Data.PokemonType target);
    }
}
