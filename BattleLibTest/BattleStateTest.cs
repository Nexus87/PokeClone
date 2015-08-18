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
            _changeEventCnt = 0;
            _state.clientCommandEvent += (a, b) =>
            {
                switch (b.Command.Type)
                {
                    case CommandType.Exit:
                        _exitEventCnt++;
                        break;
                    case CommandType.Move:
                        _actionEventCnt++;
                        break;
                    case CommandType.Change:
                        _changeEventCnt++;
                        break;
                }
            };
        }

        [Test]
        public void simpleActionTest()
        {
            _state.makeMove(_move, _client1, 0);
            _state.makeMove(_move, _client2, 0);

            Assert.AreEqual(_actionEventCnt, 2);
            Assert.AreEqual(_exitEventCnt, 0);
            Assert.AreEqual(_changeEventCnt, 0);
        }

        [Test]
        public void simpleExitTest()
        {
            _state.requestExit (_client1);
            _state.requestExit (_client2);

            Assert.AreEqual(_actionEventCnt, 0);
            Assert.AreEqual(_exitEventCnt, 2);
            Assert.AreEqual(_changeEventCnt, 0);
        }

        [Test]
        public void simpleChangeTest()
        {
            ICharakter char1 = new TestChar();
            ICharakter char2 = new TestChar();

            _state.changeChar(_client1, char1);
            _state.changeChar(_client2, char2);

            Assert.AreEqual(_actionEventCnt, 0);
            Assert.AreEqual(_exitEventCnt, 0);
            Assert.AreEqual(_changeEventCnt, 2);
        }
        [Test]
        public void mixedExitActionTest()
        {
            _state.makeMove(_move, _client1, 0);
            _state.requestExit (_client2);

            Assert.AreEqual(_actionEventCnt, 1);
            Assert.AreEqual(_exitEventCnt, 1);
            Assert.AreEqual(_changeEventCnt, 0);
        }

        [Test]
        public void multipleActionExceptionTest()
        {
            _state.makeMove(_move, _client1, 0);
            _state.makeMove(_move, _client2, 0);

            Assert.Throws<InvalidOperationException>(() => _state.makeMove(_move, _client1, 0));
            Assert.Throws<InvalidOperationException>(() => _state.makeMove(_move, _client2, 0));

            Assert.AreEqual(_actionEventCnt, 2);
            Assert.AreEqual(_exitEventCnt, 0);
            Assert.AreEqual(_changeEventCnt, 0);
        }

        [Test]
        public void multipleExitExceptionTest()
        {
            _state.requestExit (_client1);
            _state.requestExit (_client2);

            Assert.Throws<InvalidOperationException>(() => _state.requestExit (_client1));
            Assert.Throws<InvalidOperationException>(() => _state.requestExit(_client2));

            Assert.AreEqual(_actionEventCnt, 0);
            Assert.AreEqual(_exitEventCnt, 2);
            Assert.AreEqual(_changeEventCnt, 0);
        }

        [Test]
        public void mixedExitActionExceptionTest()
        {
            _state.makeMove (_move, _client1, 0);
            _state.makeMove(_move, _client2, 0);

            Assert.Throws<InvalidOperationException>(() => _state.makeMove (_move, _client1, 0));
            Assert.Throws<InvalidOperationException>(() => _state.makeMove(_move, _client2, 0));

            Assert.AreEqual(_actionEventCnt, 2);
            Assert.AreEqual(_exitEventCnt, 0);
            Assert.AreEqual(_changeEventCnt, 0);
        }

        [Test]
        public void multipleChangeExceptionTest()
        {
            ICharakter char1 = new TestChar();
            ICharakter char2 = new TestChar();

            _state.changeChar(_client1, char1);
            _state.changeChar(_client2, char2);

            Assert.Throws<InvalidOperationException>(() => _state.changeChar(_client1, char1));
            Assert.Throws<InvalidOperationException>(() => _state.changeChar(_client2, char2));

            Assert.AreEqual(_actionEventCnt, 0);
            Assert.AreEqual(_exitEventCnt, 0);
            Assert.AreEqual(_changeEventCnt, 2);
        }

        [Test]
        public void resetTest()
        {
            ICharakter char1 = new TestChar();

            _state.makeMove(_move, _client1, 0);
            Assert.Throws<InvalidOperationException>(() => _state.changeChar(_client1, char1));

            _state.makeMove(_move, _client2, 0);
            Assert.Throws<InvalidOperationException>(() => _state.requestExit(_client2));

            _state.resetState(new List<IBattleClient> { _client1, _client2 });

            Assert.DoesNotThrow(() => _state.makeMove(_move, _client1, 0));
            Assert.DoesNotThrow(() => _state.requestExit(_client2));
        }

        [Test]
        public void defaultConstructorTest()
        {
            var testState = new DefaultBattleState();

            ICharakter char1 = new TestChar();

            Assert.Throws<InvalidOperationException>(() => testState.changeChar(_client1, char1));
            Assert.Throws<InvalidOperationException>(() => testState.requestExit(_client2));

            testState.resetState(new List<IBattleClient> { _client1, _client2 });

            Assert.DoesNotThrow(() => testState.makeMove(_move, _client1, 0));
            Assert.DoesNotThrow(() => testState.requestExit(_client2));
        }

        DefaultBattleState _state;
        readonly TestClient _client1 = new TestClient();
        readonly TestClient _client2 = new TestClient();
        TestChar _char = new TestChar();
        readonly Move _move = new Move(new MoveData());

        int _actionEventCnt;
        private int _exitEventCnt;
        private int _changeEventCnt;
    }
}
