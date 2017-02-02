using Pokemon.Data;

namespace Pokemon.Services.Rules
{
    public interface ITypeTable
    {
        float GetModifier(PokemonType source, PokemonType target);
    }
}
