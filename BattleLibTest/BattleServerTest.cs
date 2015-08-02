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
	class TestChar : ICharakter {
		#region ICharakter implementation
		public bool isKO ()
		{
			return false;
		}
		public string Name {
			get {
				return "TestChar";
			}
		}
		public int HP {
			get {
				return 100;
			}
		}
		public string Status {
			get {
				return "Status";
			}
		}
		#endregion
		
	}

	class TestAction : IAction {
		#region IAction implementation
		public void applyTo (ICharakter charakter)
		{
			throw new NotImplementedException ();
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
			_server = new DefaultBattleServer (_scheduler, _client1, _client2);
			_client1 = new TestClient ();
			_client2 = new TestClient ();
		}

		[Test]
		public void nullCharakterTest() {
			_client1.Charakter = null;
			_client2.Charakter = null;

			_server.start ();
			Assert.IsTrue (_client1.RoundCnt <= 1);
			Assert.IsTrue (_client2.RoundCnt <= 1);

		}
			
	}
}

