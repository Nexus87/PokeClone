//
//  DefaultBattleState.cs
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

namespace BattleLib
{

	public class DefaultBattleState : IBattleState
	{

        public event ClientAction clientActionEvent;
        public event RequestExit clientExitEvent;
        public event NewCharakter clientChangeEvent;

        public DefaultBattleState() { initEvents(); }
		public DefaultBattleState(List<IBattleClient> clients) {
			_clients.AddRange(clients);
            initEvents();
		}

        void initEvents()
        {
            // Avoid null checks
            clientActionEvent += (a, b, c) => { };
            clientExitEvent += a => {};
            clientChangeEvent += (a, b) => { };
        }
		public void resetState(IEnumerable<IBattleClient> clients){
			_clients.Clear();
			_clients.AddRange(clients);
		}

		#region IBattleState implementation

		public bool placeAction (IAction action, IBattleClient source, int targetId)
		{
			if (!_clients.Contains (source))
				throw new InvalidOperationException ("Client already placed an action in this turn");

			_clients.Remove (source);
            clientActionEvent.Invoke(source, action, targetId);

			return true;
		}

        public bool requestExit(IBattleClient source)
        {
            if (!_clients.Contains(source))
                throw new InvalidOperationException("Client already placed an action in this turn");

             clientExitEvent(source);
            
            _clients.Remove(source);
            return true;
        }

        public bool changeChar(IBattleClient source, Base.ICharakter newCharacter)
        {
            if(!_clients.Contains(source))
                throw new InvalidOperationException("Client already placed an action in this turn");

            clientChangeEvent(source, newCharacter);
            return true;
        }

		#endregion

		readonly List<IBattleClient> _clients = new List<IBattleClient>();
    }
}

