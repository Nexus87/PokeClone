using BattleLib;
using BattleLib.Components.BattleState;
using GameEngineTest.TestUtils;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLibTest.Components.BattleState
{
    [TestFixture]
    public class BattleStateComponentTest
    {
        [TestCase]
        public void Update_StateIsDoneReturnsFromBeginningTrue_DoesNotCallStatesUpdateMethod()
        {
            var waitForCharState = CreateStateMock(BattleStates.WaitForPokemon);
            var battleStateComponent = CreateStateComponent(waitForChar: waitForCharState.Object);
            waitForCharState.SetupGet(state => state.IsDone).Returns(true);

            battleStateComponent.Update();

            waitForCharState.Verify(s => s.Update(It.IsAny<BattleData>()), Times.Never);
        }

        [TestCase]
        public void Update_InitialState_FirstStateIsWaitForChar()
        {
            var waitForCharState = CreateStateMock(BattleStates.WaitForPokemon);
            var battleStateComponent = CreateStateComponent(waitForChar: waitForCharState.Object);

            battleStateComponent.Update();

            waitForCharState.Verify(s => s.Update(It.IsAny<BattleData>()), Times.Once);
        }

        [TestCase]
        public void Update_CharStateIsDone_WaitForActionIsCalled()
        {
            var waitForCharState = CreateStateMock(BattleStates.WaitForPokemon);
            var waitForActionState = CreateStateMock(BattleStates.WaitForAction);
            var battleStateComponent = CreateStateComponent(waitForChar: waitForCharState.Object, waitForAction: waitForActionState.Object);

            waitForCharState.SetupGet(state => state.IsDone).Returns(true);

            battleStateComponent.Update();

            waitForActionState.Verify(s => s.Update(It.IsAny<BattleData>()), Times.Once);
        }

        [TestCase]
        public void Update_CharAndActionStateIsDone_ExecuteIsCalled()
        {
            var waitForCharState = CreateStateMock(BattleStates.WaitForPokemon);
            var waitForActionState = CreateStateMock(BattleStates.WaitForAction);
            var executeState = CreateStateMock(BattleStates.Execute);
            var battleStateComponent = CreateStateComponent(waitForCharState.Object, waitForActionState.Object, executeState.Object);

            waitForCharState.SetupGet(state => state.IsDone).Returns(true);
            waitForActionState.SetupGet(state => state.IsDone).Returns(true);

            battleStateComponent.Update();

            executeState.Verify(s => s.Update(It.IsAny<BattleData>()), Times.Once);
        }
        [TestCase]
        public void Update_CharAndActionExecutionStateIsDone_CharStateIsCalled()
        {
            var waitForCharState = CreateStateMock(BattleStates.WaitForPokemon);
            var waitForActionState = CreateStateMock(BattleStates.WaitForAction);
            var executeState = CreateStateMock(BattleStates.Execute);
            var battleStateComponent = CreateStateComponent(waitForCharState.Object, waitForActionState.Object, executeState.Object);

            waitForCharState.SetupGet(state => state.IsDone).Returns(true);
            waitForActionState.SetupGet(state => state.IsDone).Returns(true);

            battleStateComponent.Update();
            waitForCharState.Verify(s => s.Update(It.IsAny<BattleData>()), Times.Never);

            waitForCharState.SetupGet(state => state.IsDone).Returns(false);
            executeState.SetupGet(state => state.IsDone).Returns(true);

            battleStateComponent.Update();
            waitForCharState.Verify(state => state.Update(It.IsAny<BattleData>()), Times.Once);
        }

        [TestCase]
        public void Update_OnChangingState_RaiseEvent()
        {
            bool eventRaised = false;
            var waitForCharState = CreateStateMock(BattleStates.WaitForPokemon);
            var battleStateComponent = CreateStateComponent(waitForChar: waitForCharState.Object);
            
            waitForCharState.SetupGet(state => state.IsDone).Returns(true);
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
            var battleStateComponent = CreateStateComponent(waitForChar: waitForCharState.Object, waitForAction: waitForActionState.Object);

            waitForCharState.SetupGet(state => state.IsDone).Returns(true);
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
            var battleStateComponent = CreateStateComponent(waitForCharState.Object, waitForActionState.Object, executeState.Object);

            waitForCharState.SetupGet(state => state.IsDone).Returns(true);
            waitForActionState.SetupGet(state => state.IsDone).Returns(true);

            battleStateComponent.StateChanged += (o, args) => sentArgs = args;

            battleStateComponent.Update();
            
            Assert.NotNull(sentArgs);
            Assert.AreEqual(BattleStates.Execute, sentArgs.NewState);
        }

        private Mock<IBattleState> CreateStateMock(BattleStates state)
        {
            var mock = new Mock<IBattleState>();
            mock.SetupGet(s => s.State).Returns(state);

            return mock;
        }
        private BattleStateComponent CreateStateComponent(IBattleState waitForChar = null, IBattleState waitForAction = null, IBattleState execute = null, IEventCreator creator = null)
        {
            if (waitForChar == null)
                waitForChar = CreateStateMock(BattleStates.WaitForPokemon).Object;
            if (waitForAction == null)
                waitForAction = CreateStateMock(BattleStates.WaitForAction).Object;
            if (execute == null)
                execute = CreateStateMock(BattleStates.Execute).Object;
            if(creator == null)
                creator = new Mock<IEventCreator>().Object;

            var state = new BattleStateComponent(new BattleData(), waitForAction, waitForChar, execute, creator);
            state.Initialize();
            return state;
        }
    }
}
