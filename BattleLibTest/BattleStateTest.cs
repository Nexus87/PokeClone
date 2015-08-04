using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using BattleLib;
namespace BattleLibTest
{
    [TestFixture]
    public class BattleStateTest
    {
        [SetUp]
        public void init()
        {
            _client1.Charakter = _char;
            _client2.Charakter = _char;
            List<IBattleClient> clients = new List<IBattleClient> { _client1, _client2 };

            _state = new DefaultBattleState(clients);

            _actionEventCnt = 0;
            _exitEventCnt = 0;

            _state.clientActionEvent += (source, a, id) => _actionEventCnt++;
            _state.clientExitEvent += (source) => _exitEventCnt++;
        }

        [Test]
        public void simpleActionTest()
        {
            _client1.Action = (IBattleState state) => state.placeAction(_action, _client1, 0);
            _client2.Action = (IBattleState state) => state.placeAction(_action, _client2, 0);

            _client1.requestAction(_state);
            _client2.requestAction(_state);

            Assert.AreEqual(_actionEventCnt, 2);
            Assert.AreEqual(_exitEventCnt, 0);
        }

        [Test]
        public void simpleExitTest()
        {
            _client1.Action = (IBattleState state) => state.requestExit(_client1);
            _client2.Action = (IBattleState state) => state.requestExit(_client2);

            _client1.requestAction(_state);
            _client2.requestAction(_state);

            Assert.AreEqual(_actionEventCnt, 0);
            Assert.AreEqual(_exitEventCnt, 2);
        }

        [Test]
        public void mixedExitActionTest()
        {
            _client1.Action = (IBattleState state) => state.placeAction(_action, _client1, 0);
            _client2.Action = (IBattleState state) => state.requestExit(_client2);
            
            _client1.requestAction(_state);
            _client2.requestAction(_state);

            Assert.AreEqual(_actionEventCnt, 1);
            Assert.AreEqual(_exitEventCnt, 1);
        }

        [Test]
        public void multipleActionExceptionTest()
        {
            _client1.Action = (IBattleState state) => state.placeAction(_action, _client1, 0);
            _client2.Action = (IBattleState state) => state.placeAction(_action, _client2, 0);

            _client1.requestAction(_state);
            Assert.Throws<InvalidOperationException>(() => _client1.requestAction(_state));
            _client2.requestAction(_state);

            Assert.AreEqual(_actionEventCnt, 2);
            Assert.AreEqual(_exitEventCnt, 0);
        }

        [Test]
        public void multipleExitExceptionTest()
        {
            _client1.Action = (IBattleState state) => state.requestExit(_client1);
            _client2.Action = (IBattleState state) => state.requestExit(_client2);

            _client1.requestAction(_state);
            Assert.Throws<InvalidOperationException>(() => _client1.requestAction(_state));
            _client2.requestAction(_state);

            Assert.AreEqual(_actionEventCnt, 0);
            Assert.AreEqual(_exitEventCnt, 2);
        }

        [Test]
        public void mixedExitActionExceptionTest()
        {
            _client1.Action = (IBattleState state) => state.placeAction(_action, _client1, 0);
            _client2.Action = (IBattleState state) => state.placeAction(_action, _client2, 0);

            _client1.requestAction(_state);

            _client1.Action = (IBattleState state) => state.requestExit(_client1);
            Assert.Throws<InvalidOperationException>(() => _client1.requestAction(_state));

            _client2.requestAction(_state);

            Assert.AreEqual(_actionEventCnt, 2);
            Assert.AreEqual(_exitEventCnt, 0);
        }
        DefaultBattleState _state;
        TestClient _client1 = new TestClient();
        TestClient _client2 = new TestClient();
        TestChar _char = new TestChar();
        TestAction _action = new TestAction();

        int _actionEventCnt;
        int _exitEventCnt;
    }
}
