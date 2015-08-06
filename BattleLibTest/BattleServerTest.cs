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
using System.Threading;
using System.Collections.Generic;

using NUnit.Framework;
using BattleLib;

namespace BattleLibTest
{
    delegate void ApplyDelegate(ICharakter charakter);

	class TestAction : IAction {
        public ApplyDelegate Action { get; set; }
		#region IAction implementation
		public void applyTo (ICharakter charakter)
		{
            if (Action != null)
                Action(charakter);
		}
		public ActionType Type {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}
		#endregion
		
	}

	public class TestScheduler : IActionScheduler {
		readonly List<ActionData> _actions = new List<ActionData>();

		#region IActionScheduler implementation
		public void appendAction (ActionData action)
		{
			_actions.Add (action);
		}
		public void appendAction (IEnumerable<ActionData> actions)
		{
			_actions.AddRange (actions);
		}

		public void clearActions ()
		{
			_actions.Clear ();
		}
		public IEnumerable<ActionData> schedulActions ()
		{
			return _actions;
		}
		#endregion
	}

	[TestFixture]
	public class BattleServerTest
	{
		TestClient _client1;
		TestClient _client2;
		TestScheduler _scheduler;
		IBattleServer _server;

		[SetUp]
		public void init() {
			_scheduler = new TestScheduler ();
			_client1 = new TestClient ();
			_client2 = new TestClient ();
            _server = new DefaultBattleServer(_scheduler, _client1, _client2);
		}

		[Test]
		public void NullCharakterTest() {
            
			_client1.Charakter = null;
			_client2.Charakter = null;

			_server.start ();
			Assert.IsTrue (_client1.RoundCnt <= 1);
			Assert.IsTrue (_client2.RoundCnt <= 1);
		}

		[Test]
        public void ConstructorExceptionTest()
        {
            Assert.Throws<NullReferenceException>(() => new DefaultBattleServer(_scheduler, null));
            Assert.Throws<NullReferenceException>(() => new DefaultBattleServer(null, _client1));
            Assert.Throws<NullReferenceException>(() => new DefaultBattleServer(null, null));
            Assert.Throws<NullReferenceException>(() => new DefaultBattleServer(_scheduler, null, null));
            Assert.Throws<NullReferenceException>(() => new DefaultBattleServer(_scheduler, _client1, null));
            Assert.Throws<NullReferenceException>(() => new DefaultBattleServer(_scheduler, null, _client1));
        }

        [Test]
        public void StartExceptionTest()
        {
            var testServer = new DefaultBattleServer(_scheduler);
            Assert.Throws<InvalidOperationException>(() => testServer.start());

            testServer = new DefaultBattleServer(_scheduler, _client1);
            Assert.Throws<InvalidOperationException>(() => testServer.start());
        }

        [Test]
        public void fullBattleTest()
        {
            _client1.Charakter = new TestChar();
            _client2.Charakter = new TestChar();

            TestAction action1 = new TestAction { Action = (ICharakter charakter) => ((TestChar)charakter).HP = 0 };
            TestAction action2 = new TestAction();

            // TODO don't hardcode the target id
            _client1.Action = (IBattleState state) => state.placeAction(action1, _client1, 1);
            _client2.Action = (IBattleState state) => state.placeAction(action2, _client2, 0);

            _server.start();

            Assert.AreEqual(_client1.RoundCnt, 1);
            Assert.AreEqual(_client2.RoundCnt, 1);

            Assert.AreEqual(_client2.Charakter.HP, 0);
            
        }
	}
}

