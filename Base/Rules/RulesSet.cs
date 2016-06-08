
namespace Base.Rules
{
    public class RulesSet
    {
        public IBattleRules BattleRules { get; private set; }
        public IPokemonRules PokemonRules { get; private set; }
        public ITypeTable TypeTable { get; private set; }
        public IMoveEffectCalculator MoveCalculator { get; private set; }

        public RulesSet(IBattleRules battleRules, IPokemonRules pokemonRules, ITypeTable typeTable, IMoveEffectCalculator moveCalculator)
        {
            BattleRules = battleRules;
            PokemonRules = pokemonRules;
            TypeTable = typeTable;
            MoveCalculator = moveCalculator;
        }
    }
}
