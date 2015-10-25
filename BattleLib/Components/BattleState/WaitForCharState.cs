using Base;
using System;

namespace BattleLib.Components.BattleState
{
    public class WaitForCharState : IBattleState
    {
        bool done = false;

        ClientIdentifier player;
        ClientIdentifier ai;

        Pokemon enqueuedPlayerChar = null;
        Pokemon enqueuedAiChar = null;

        public WaitForCharState(ClientIdentifier player, ClientIdentifier ai)
        {
            this.player = player;
            this.ai = ai;
        }

        public void Update(BattleData data)
        {
            if (data.playerPkmn != null && enqueuedPlayerChar != null)
                throw new InvalidOperationException("Player character is already set!\n");
            if(data.aiPkmn != null && enqueuedAiChar != null)
                throw new InvalidOperationException("AI character is already set!\n");

            if (enqueuedPlayerChar != null)
            {
                data.playerPkmn = enqueuedPlayerChar;
                enqueuedPlayerChar = null;
            }

            if (enqueuedAiChar != null)
            {
                data.aiPkmn = enqueuedAiChar;
                enqueuedAiChar = null;
            }

            done = data.playerPkmn != null && data.aiPkmn != null;
        }

        public void SetMove(ClientIdentifier id, Move move)
        {
            // Does nothing
        }

        public void SetItem(ClientIdentifier id, Item item)
        {
            // Does nothing
        }


        public void SetCharacter(ClientIdentifier id, Pokemon pkmn)
        {
            if(id == player)
                enqueuedPlayerChar = pkmn;
            else if(id == ai)
                enqueuedAiChar = pkmn;
            else
                throw new ArgumentException("Source identifier not found\n");
        }

        public bool IsDone()
        {
            return done;
        }
    }
}
