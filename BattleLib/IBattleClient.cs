using System;

namespace BattleLib
{
	public interface IBattleClient
	{
		string ClientName { get; }
		int Id { get; }

		void battleStartHandler(object sender, EventArgs args);
		void newRoundHandler(object sender, EventArgs args);
		void battleEndHandler(object sender, EventArgs args);
	}
}

