using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.Menu
{
    internal class NullMenuModel : IMenuModel
    {
        public MenuType Type{ get{ return MenuType.None; }}

        public IEnumerator<string> GetEnumerator()
        {
            return Enumerable.Empty<string>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Enumerable.Empty<string>().GetEnumerator();
        }
    }
}
