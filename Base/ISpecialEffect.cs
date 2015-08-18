using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public interface ISpecialEffect
    {
        void executeEffect(EffectFacade facade);
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
		public void executeEffect (EffectFacade facade)
		{
			if (Self)
				facade.manipulateSourceState (State, Modifier);
			else
				facade.manipulateTargetState (State, Modifier);
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
		public void executeEffect (EffectFacade facade)
		{
			if (Self)
				facade.manipulateSourceCondition (Condition);
			else
				facade.manipulateTargetCondition (Condition);
		}
		#endregion
	}
}
