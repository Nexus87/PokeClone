﻿using System;
using Base;
namespace BattleLib
{
	public enum ActionType {
		immediate,
		ranked
	}
		
	public interface IAction
	{
		void applyTo(ICharakter charakter);
		ActionType Type { get; set; }
	}
}
