//
//  TestClient.cs
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

using BattleLib;

namespace BattleLibTest
{
	class TestClient : IBattleClient {
		int _roundNumber = 0;
		int _roundCnt = 0;
		public int RoundNumbers { 
			get{ 
				return _roundNumber;
			} 
			set{
				_roundNumber = value; 
				_roundCnt = 0;
			} 
		}
		public int RoundCnt{  
			get{
				return _roundCnt;
			}
		}

		public ICharakter Charakter { get; set; }
		#region IBattleClient implementation
		public void requestAction (IBattleState state)
		{
		}
		public ICharakter requestCharakter ()
		{
			return Charakter;
		}
		public string ClientName {
			get {
				return "TestClient";
			}
		}
		#endregion


	}
}

