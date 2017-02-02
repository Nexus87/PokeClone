using Pokemon.Models;

namespace Pokemon.Services.Rules
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