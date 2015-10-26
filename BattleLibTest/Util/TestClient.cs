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

using Base;
using BattleLib.Interfaces;
namespace BattleLibTest
{

    class TestClient : AbstractClient {
		int _roundNumber;
		int _roundCnt;
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

        public Pokemon Charakter { get; set; }
        public IClientCommand Command { get; set; }

		#region IBattleClient implementation

        public override IClientCommand RequestAction()
		{            
            _roundCnt++;
            return Command;
		}
        public override Pokemon RequestCharacter()
		{
            if (Charakter == null || Charakter.IsKO())
                return null;
			return Charakter;
		}

        public override string ClientName
        {
			get {
				return "TestClient";
			}
		}
		#endregion


	}
}

