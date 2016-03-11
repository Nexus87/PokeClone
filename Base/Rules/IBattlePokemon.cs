using Base.Data;

namespace Base.Rules
{
    public interface IBattlePokemon
    {
        float Accuracy { get; }
        int Atk { get; }
        StatusCondition Condition { get; }

        int Def { get; }
        float Evasion { get; }
        int HP { get; }
        int ID { get; }
        int Level { get; }
        int MaxHP { get; }
        string Name { get; }

        int SpAtk { get; }
        int SpDef { get; }

        PokemonType Type1 { get; }
        PokemonType Type2 { get; }
    }
}