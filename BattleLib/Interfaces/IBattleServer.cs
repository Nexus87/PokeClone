using System;
using System.Collections.Generic;
using Base;
namespace BattleLib
{
	public delegate void Event(Object sender, EventArgs args);

	public interface IBattleServer
	{
		void start();
		IBattleObserver getObserver();
	}
}

