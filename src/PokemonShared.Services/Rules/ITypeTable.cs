using PokemonShared.Data;

namespace PokemonShared.Services.Rules
{
    public interface ITypeTable
    {
        float GetModifier(PokemonType source, PokemonType target);
    }
}
