using BattleMode.Entities.BattleState;
using FakeItEasy;
using GameEngineTest.TestUtils;
using NUnit.Framework;

namespace BattleModeTest.Components.BattleState
{
    [TestFixture]
    public class BattleStateComponentTest
    {
        [TestCase]
        public void Update_StateIsDoneReturnsFromBeginningTrue_DoesNotCallStatesUpdateMethod()
        {
            var waitForCharState = CreateStateMock(BattleStates.WaitForPokemon);
            var battleStateComponent = CreateStateComponent(waitForCharState);
            A.CallTo(() => waitForCharState.IsDone).Returns(true);

            battleStateComponent.Update();

            A.CallTo(() => waitForCharState.Update(A<BattleData>.Ignored)).MustNotHaveHappened();
        }

        [TestCase]
        public void Update_InitialState_FirstStateIsWaitForChar()
        {
            var waitForCharState = CreateStateMock(BattleStates.WaitForPokemon);
            var battleStateComponent = CreateStateComponent(waitForCharState);

            battleStateComponent.Update();

            A.CallTo(() => waitForCharState.Update(A<BattleData>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase]
        public void Update_CharStateIsDone_WaitForActionIsCalled()
        {
            var waitForCharState = CreateStateMock(BattleStates.WaitForPokemon);
            var waitForActionState = CreateStateMock(BattleStates.WaitForAction);
            var battleStateComponent = CreateStateComponent(waitForCharState, waitForActionState);

            A.CallTo(() => waitForCharState.IsDone).Returns(true);

            battleStateComponent.Update();

            A.CallTo(() => waitForActionState.Update(A<BattleData>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase]
        public void Update_CharAndActionStateIsDone_ExecuteIsCalled()
        {
            var waitForCharState = CreateStateMock(BattleStates.WaitForPokemon);
            var waitForActionState = CreateStateMock(BattleStates.WaitForAction);
            var executeState = CreateStateMock(BattleStates.Execute);
            var battleStateComponent = CreateStateComponent(waitForCharState, waitForActionState, executeState);

            A.CallTo(() => waitForCharState.IsDone).Returns(true);
            A.CallTo(() => waitForActionState.IsDone).Returns(true);

            battleStateComponent.Update();

            A.CallTo(() => executeState.Update(A<BattleData>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }
        [TestCase]
        public void Update_CharAndActionExecutionStateIsDone_CharStateIsCalled()
        {
            var waitForCharState = CreateStateMock(BattleStates.WaitForPokemon);
            var waitForActionState = CreateStateMock(BattleStates.WaitForAction);
            var executeState = CreateStateMock(BattleStates.Execute);
            var battleStateComponent = CreateStateComponent(waitForCharState, waitForActionState, executeState);

            A.CallTo(() => waitForCharState.IsDone).Returns(true);
            A.CallTo(() => waitForActionState.IsDone).Returns(true);

            battleStateComponent.Update();
            A.CallTo(() => waitForCharState.Update(A<BattleData>.Ignored)).MustNotHaveHappened();

            A.CallTo(() => waitForCharState.IsDone).Returns(false);
            A.CallTo(() => executeState.IsDone).Returns(true);

            battleStateComponent.Update();
            A.CallTo(() => waitForCharState.Update(A<BattleData>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase]
        public void Update_OnChangingState_RaiseEvent()
        {
            var eventRaised = false;
            var waitForCharState = CreateStateMock(BattleStates.WaitForPokemon);
            var battleStateComponent = CreateStateComponent(waitForCharState);
            
            A.CallTo(() => waitForCharState.IsDone).Returns(true);
            battleStateComponent.StateChanged += delegate { eventRaised = true; };

            battleStateComponent.Update();

            Assert.True(eventRaised);
        }

        [TestCase]
        public void Update_ChangeEvents_ArgumentsSignalRightState()
        {
            StateChangedEventArgs sentArgs = null;
            var waitForCharState = CreateStateMock(BattleStates.WaitForPokemon);
            var waitForActionState = CreateStateMock(BattleStates.WaitForAction);
            var battleStateComponent = CreateStateComponent(waitForCharState, waitForActionState);

            A.CallTo(() => waitForCharState.IsDone).Returns(true);
            battleStateComponent.StateChanged += (o, args) => sentArgs = args;

            battleStateComponent.Update();

            Assert.NotNull(sentArgs);
            Assert.AreEqual(BattleStates.WaitForAction, sentArgs.NewState);
        }

        [TestCase]
        public void Update_NextStateInitalIsDone_NoEventIsRaisedForThisState()
        {
            StateChangedEventArgs sentArgs = null;
            var waitForCharState = CreateStateMock(BattleStates.WaitForPokemon);
            var waitForActionState = CreateStateMock(BattleStates.WaitForAction);
            var executeState = CreateStateMock(BattleStates.Execute);
            var battleStateComponent = CreateStateComponent(waitForCharState, waitForActionState, executeState);

            A.CallTo(() => waitForCharState.IsDone).Returns(true);
            A.CallTo(() => waitForActionState.IsDone).Returns(true);

            battleStateComponent.StateChanged += (o, args) => sentArgs = args;

            battleStateComponent.Update();
            
            Assert.NotNull(sentArgs);
            Assert.AreEqual(BattleStates.Execute, sentArgs.NewState);
        }

        private static IBattleState CreateStateMock(BattleStates state)
        {
            var mock = A.Fake<IBattleState>();
            A.CallTo(() => mock.State).Returns(state);

            return mock;
        }

        private static BattleStateComponent CreateStateComponent(IBattleState waitForChar = null, IBattleState waitForAction = null, IBattleState execute = null, IEventCreator creator = null)
        {
            if (waitForChar == null)
                waitForChar = CreateStateMock(BattleStates.WaitForPokemon);
            if (waitForAction == null)
                waitForAction = CreateStateMock(BattleStates.WaitForAction);
            if (execute == null)
                execute = CreateStateMock(BattleStates.Execute);
            if(creator == null)
                creator = A.Fake<IEventCreator>();

            var state = new BattleStateComponent(new BattleData(), waitForAction, waitForChar, execute, creator);
            state.Initialize();
            return state;
        }
    }
}
