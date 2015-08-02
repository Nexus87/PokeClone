using System;
using System.Collections.Generic;

namespace BattleLib
{
	public delegate void Event(Object sender, EventArgs args);
	public enum ServerState {
		WaitForClientRegister,
		WaitForClientAction,
		WaitForClientCharakter,
		ServerStopped
	}

	public interface IBattleServer
	{
		void start();
		IBattleObserver getObserver();


	}
}

