namespace Base
{
    public enum State{
        HP,
        Atk,
        Def,
        SpAtk,
        SpDef,
        Speed
    }
    public interface IEffectFacade
    {
        void ManipulateTargetState(State stat, int modifier);
		void ManipulateSourceState(State stat, int modifier);
        void ManipulateTargetCondition(StatusCondition condition);
		void ManipulateSourceCondition(StatusCondition condition);
    }
}
