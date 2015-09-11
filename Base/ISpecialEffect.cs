using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public interface ISpecialEffect
    {
        void ExecuteEffect(IEffectFacade facade);
    }

	public class StateEffect : ISpecialEffect {
		State State { get; set; }
		int Modifier { get; set; }
		bool Self { get; set; }

		public StateEffect(State state, int modifier, bool self) {
			State = state;
			Modifier = modifier;
			Self = self;
		}

		#region ISpecialEffect implementation
		public void ExecuteEffect (IEffectFacade facade)
		{
			if (Self)
				facade.ManipulateSourceState (State, Modifier);
			else
				facade.ManipulateTargetState (State, Modifier);
		}
		#endregion
	}

	public class ConditionEffect : ISpecialEffect {
		StatusCondition Condition { get; set; }
		bool Self { get; set; }

		public ConditionEffect(StatusCondition condition, bool self){
			Condition = condition;
			Self = self;
		}
		#region ISpecialEffect implementation
		public void ExecuteEffect (IEffectFacade facade)
		{
			if (Self)
				facade.ManipulateSourceCondition (Condition);
			else
				facade.ManipulateTargetCondition (Condition);
		}
		#endregion
	}
}
