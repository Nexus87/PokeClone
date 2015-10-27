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
        int playerPkmnId;
        int aiPkmnId;

        string playerPkmnName;
        string aiPkmnName;

        StatusCondition playerPkmnCondition;
        StatusCondition aiPkmnCondition;

        int playerHP;
        int aiHP;

    }
}
