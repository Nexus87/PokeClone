namespace BattleMode.Shared
{
    public class HpChangedEventArgs
    {
        public HpChangedEventArgs(PokemonEntity source, int oldHp, int newHp)
        {
            Source = source;
            OldHp = oldHp;
            NewHp = newHp;
        }

        public int NewHp { get; }
        public int OldHp { get; }
        public PokemonEntity Source { get; }
    }
}