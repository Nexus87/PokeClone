using System;
using System.Collections.Generic;

using NUnit.Framework;
using BattleLib;
using Base;
using BattleLib.Interfaces;
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
            var clients = new List<IBattleClient> { _client1, _client2 };

            _state = new DefaultBattleState(clients);

            _actionEventCnt = 0;
            _exitEventCnt = 0;
            _state.clientCommandEvent += (a, b) =>
            {
                if (b.Command.Type == CommandType.Exit)
                    _exitEventCnt++;
                else if (b.Command.Type == CommandType.Move)
                    _actionEventCnt++;
            };
        }

        [Test]
        public void simpleActionTest()
        {
            _client1.Action = state => state.makeMove(_move, _client1, 0);
            _client2.Action = state => state.makeMove(_move, _client2, 0);

            _client1.requestAction(_state);
            _client2.requestAction(_state);

            Assert.AreEqual(_actionEventCnt, 2);
            Assert.AreEqual(_exitEventCnt, 0);
        }

        [Test]
        public void simpleExitTest()
        {
            _client1.Action = state => state.requestExit (_client1);
            _client2.Action = state => state.requestExit (_client2);

            _client1.requestAction(_state);
            _client2.requestAction(_state);

            Assert.AreEqual(_actionEventCnt, 0);
            Assert.AreEqual(_exitEventCnt, 2);
        }

        [Test]
        public void mixedExitActionTest()
        {
            _client1.Action = state => state.makeMove(_move, _client1, 0);
            _client2.Action = state => state.requestExit (_client2);
            
            _client1.requestAction(_state);
            _client2.requestAction(_state);

            Assert.AreEqual(_actionEventCnt, 1);
            Assert.AreEqual(_exitEventCnt, 1);
        }

        [Test]
        public void multipleActionExceptionTest()
        {
            _client1.Action = state => state.makeMove(_move, _client1, 0);
            _client2.Action = state => state.makeMove(_move, _client2, 0);

            _client1.requestAction(_state);
            Assert.Throws<InvalidOperationException>(() => _client1.requestAction(_state));
            _client2.requestAction(_state);

            Assert.AreEqual(_actionEventCnt, 2);
            Assert.AreEqual(_exitEventCnt, 0);
        }

        [Test]
        public void multipleExitExceptionTest()
        {
            _client1.Action = state => state.requestExit (_client1);
            _client2.Action = state => state.requestExit (_client2);

            _client1.requestAction(_state);
            Assert.Throws<InvalidOperationException>(() => _client1.requestAction(_state));
            _client2.requestAction(_state);

            Assert.AreEqual(_actionEventCnt, 0);
            Assert.AreEqual(_exitEventCnt, 2);
        }

        [Test]
        public void mixedExitActionExceptionTest()
        {
            _client1.Action = state => state.makeMove (_move, _client1, 0);
            _client2.Action = state => state.makeMove(_move, _client2, 0);

            _client1.requestAction(_state);

            _client1.Action = state => state.requestExit (_client1);
            Assert.Throws<InvalidOperationException>(() => _client1.requestAction(_state));

            _client2.requestAction(_state);

            Assert.AreEqual(_actionEventCnt, 2);
            Assert.AreEqual(_exitEventCnt, 0);
        }

        DefaultBattleState _state;
        readonly TestClient _client1 = new TestClient();
        readonly TestClient _client2 = new TestClient();
        TestChar _char = new TestChar();
        readonly Move _move = new Move(new MoveData());

        int _actionEventCnt;
private  int _exitEventCnt;
    }
}
