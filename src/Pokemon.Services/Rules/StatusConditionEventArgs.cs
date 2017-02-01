using Base.Data;

namespace Base.Rules
{
    public class StatusConditionEventArgs
    {
        public StatusConditionEventArgs(StatusCondition newCondition, PokemonEntity source)
        {
            NewCondition = newCondition;
            Source = source;
        }

        public PokemonEntity Source { get; }
        public StatusCondition NewCondition { get; }
    }
}