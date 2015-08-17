﻿//
//  IBattleState.cs
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
using Base;
namespace BattleLib
{
    public delegate void ClientAction(IBattleClient source, IAction action, int targetId);
    public delegate void RequestExit(IBattleClient source);
    public delegate void NewCharakter(IBattleClient source, ICharakter newCharakter);

	public interface IBattleState
	{
        event ClientAction clientActionEvent;
        event RequestExit clientExitEvent;
        event NewCharakter clientChangeEvent;

		bool placeAction(IAction action, IBattleClient source, int targetId);
        bool changeChar(IBattleClient source, ICharakter newCharacter);
        bool requestExit(IBattleClient source);
	}
}

