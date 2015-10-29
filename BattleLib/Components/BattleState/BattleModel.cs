using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleState
{
    public class BattleModel
    {
        // TODO Send informations about what has changed
        public event EventHandler<Object> OnDataChange = (a,b) => {};

        int playerPkmn;
        int aiPkmn;

        string playerName;
        string aiName;

        StatusCondition playerCondition;
        StatusCondition aiCondition;

        float playerHP;
        float aiHP;

        public int PlayerPkmn { get { return playerPkmn; } set { playerPkmn = value;  OnDataChange(this, null); } }
        public int AIPkmn { get { return aiPkmn; } set { aiPkmn = value; OnDataChange(this, null); } }

        public string PlayerName { get { return playerName; } set { playerName = value; OnDataChange(this, null); } }
        public string AIName { get { return aiName; } set { aiName = value; OnDataChange(this, null); } }

        public StatusCondition PlayerCondition { get { return playerCondition; } set { playerCondition = value; OnDataChange(this, null); } }
        public StatusCondition AICondition { get { return aiCondition; } set { aiCondition = value; OnDataChange(this, null); } }

        public float PlayerHP { get { return playerHP; } set { playerHP = value;  OnDataChange(this, null); } }
        public float AIHP { get { return aiHP; } set { aiHP = value;  OnDataChange(this, null); } } 

    }
}
