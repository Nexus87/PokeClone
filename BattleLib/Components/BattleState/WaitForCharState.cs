using Base;
using System;

namespace BattleLib.Components.BattleState
{
    public class WaitForCharState : AbstractState
    {
        bool done = false;

        ClientIdentifier player;
        ClientIdentifier ai;

        Pokemon enqueuedPlayerChar = null;
        Pokemon enqueuedAiChar = null;

        BattleStateComponent state;
        public WaitForCharState(BattleStateComponent state, ClientIdentifier player, ClientIdentifier ai)
        {
            this.player = player;
            this.ai = ai;
            this.state = state;
        }

        public override void Init()
        {
            enqueuedAiChar = null;
            enqueuedPlayerChar = null;
            done = false;
        }

        public override IBattleState Update(BattleData data)
        {
            done = data.PlayerPkmn != null && data.AIPkmn != null;
            if (done)
                return state.actionState;

            if (data.PlayerPkmn == null && enqueuedPlayerChar != null)
                data.PlayerPkmn.Pokemon = enqueuedPlayerChar;

            if (data.AIPkmn == null && enqueuedAiChar != null)
                data.AIPkmn.Pokemon = enqueuedAiChar;

            return this;
        }

        public override void SetCharacter(ClientIdentifier id, Pokemon pkmn)
        {
            if (id == player)
            {
                if (enqueuedPlayerChar != null)
                    throw new InvalidOperationException("Character is already set for player.");
                enqueuedPlayerChar = pkmn;
            }
            else if (id == ai)
            {
                if (enqueuedAiChar != null)
                    throw new InvalidOperationException("Character is already set for AI.");
                enqueuedAiChar = pkmn;
            }
            else
                throw new ArgumentException("Source identifier not found\n");
        }

        public bool IsDone()
        {
            return done;
        }
    }
}
