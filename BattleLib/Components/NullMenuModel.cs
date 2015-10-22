using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components
{
    internal class NullMenuModel : IMenuModel
    {
        public event EventHandler<SelectionEventArgs> OnSelectionChanged;

        public MenuType Type{ get{ return MenuType.None; }}

        public MenuType Select() { return Type; }

        public MenuType Back() { return Type; }

        public void HandleDirection(Direction direction) { }

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
