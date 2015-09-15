//
//  BattleServerTest.cs
//
//  Author:
//       Nexxuz0 <>
//
//  Copyright (c) 2015 Nexxuz0
//
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//
using System;
using System.Collections.Generic;
using Base;
using NUnit.Framework;
using BattleLib;
using BattleLib.Interfaces;

namespace BattleLibTest
{
    delegate void ApplyDelegate(ICharacter charakter);

	public class TestScheduler : ICommandScheduler {
        readonly List<IClientCommand> _commands = new List<IClientCommand>();

        public void AppendCommand(BattleLib.Interfaces.IClientCommand command)
        {
            _commands.Add(command);
        }

        public void AppendCommand(IEnumerable<BattleLib.Interfaces.IClientCommand> commands)
        {
            _commands.AddRange(commands);
        }

        public IEnumerable<BattleLib.Interfaces.IClientCommand> ScheduleCommands()
        {
            return _commands;
        }


        public void ClearCommands()
        {
            _commands.Clear();
        }
    }

    public class TestRules : IBattleRules
    {

        public bool CanEscape()
        {
            return true;
        }

        public bool CanChange()
        {
            return true;
        }

        public bool ExecMove(ICharacter source, Move move, ICharacter target)
        {
            return true;
        }
    }

    class TestClient1 : TestClient
    {
        public override IClientCommand RequestAction()
        {
            if (RoundCnt == 0)
                Command =MoveCommand(new Move(new MoveData()), 1);
            else
                Command = ExitCommand();

            return base.RequestAction();
        }
    }
    class TestClient2 : TestClient
    {
        public override IClientCommand RequestAction()
        {
            Command =  MoveCommand(new Move(new MoveData()), 1);
            return base.RequestAction();
        }
    }
	[TestFixture]
	public class BattleServerTest
	{
		TestClient1 _client1;
		TestClient2 _client2;
		TestScheduler _scheduler;
		IBattleServer _server;

		[SetUp]
		public void init() {
			_scheduler = new TestScheduler ();
			_client1 = new TestClient1 ();
			_client2 = new TestClient2 ();
            _server = new DefaultBattleServer(_scheduler, new TestRules(), _client1, _client2);
		}

		[Test]
		public void NullCharakterTest() {
            
			_client1.Charakter = null;
			_client2.Charakter = null;

			_server.Start ();
			Assert.IsTrue (_client1.RoundCnt <= 1);
			Assert.IsTrue (_client2.RoundCnt <= 1);
		}

		[Test]
        public void ConstructorExceptionTest()
        {
            IBattleRules rules = new TestRules();
            Assert.Throws<NullReferenceException>(() => new DefaultBattleServer(_scheduler, rules, null));
            Assert.Throws<NullReferenceException>(() => new DefaultBattleServer(null, rules, _client1));
            Assert.Throws<NullReferenceException>(() => new DefaultBattleServer(_scheduler, null, _client1));
            Assert.Throws<NullReferenceException>(() => new DefaultBattleServer(null, rules,  null));
            Assert.Throws<NullReferenceException>(() => new DefaultBattleServer(null, null, null));
            Assert.Throws<NullReferenceException>(() => new DefaultBattleServer(_scheduler, rules, null, null));
            Assert.Throws<NullReferenceException>(() => new DefaultBattleServer(_scheduler, rules, _client1, null));
            Assert.Throws<NullReferenceException>(() => new DefaultBattleServer(_scheduler, rules, null, _client1));
        }

        [Test]
        public void StartExceptionTest()
        {
            IBattleRules rules = new TestRules();
            var testServer = new DefaultBattleServer(_scheduler, rules);
			Assert.Throws<InvalidOperationException> (testServer.Start);

            testServer = new DefaultBattleServer(_scheduler, rules, _client1);
			Assert.Throws<InvalidOperationException> (testServer.Start);
        }

        [Test]
        public void fullBattleTest()
        {
            _client1.Charakter = new TestChar();
            _client2.Charakter = new TestChar();

            // TODO don't hardcode the target id

            _server.Start();

            Assert.AreEqual(_client1.RoundCnt, 2);
            Assert.AreEqual(_client2.RoundCnt, 2);
            
        }
	}
}

