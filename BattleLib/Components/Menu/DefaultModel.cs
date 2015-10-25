using System.Collections;
using System.Collections.Generic;

namespace BattleLib.Components.Menu
{
    public class DefaultModel<T> : IMenuModel
    {
        List<T> items;

        // TODO replace with [] operator
        public T Get(int index)
        {
            return items[index];
        }
        public DefaultModel(List<T> items, MenuType type)
        {
            this.items = items;
            Type = type;
        }

        public MenuType Type { get; private set; }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var item in items)
                yield return item.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
