using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.Menu
{
    public enum MenuType
    {
        Main,
        Attack,
        PKMN,
        Item,
        None
    }


    public interface IMenuModel : IEnumerable<String>
    {
        MenuType Type { get; }
    }
}
