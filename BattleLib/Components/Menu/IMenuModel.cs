using System;
using System.Collections.Generic;

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
