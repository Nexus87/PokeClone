using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components
{
    public class CharacterMenuModel : IMenuModel
    {
        List<Pokemon> list;
        int selectedIndex = 0;

        public CharacterMenuModel(List<Pokemon> list)
        {
            this.list = list;
        }

        public CharacterMenuModel()
        {
            list = new List<Pokemon>();
            PKData data = new PKData();
            Stats stats = new Stats();

            for (int i = 0; i < 6; i++)
            {
                list.Add(new Pokemon(data, 10, "Name" + (i + 1), stats, stats));
            }
        }
        public event EventHandler<SelectionEventArgs> OnSelectionChanged;

        public MenuType Type
        {
            get { return MenuType.PKMN; }
        }

        public MenuType Select()
        {
            throw new NotImplementedException();
        }

        public MenuType Back()
        {
            return MenuType.Main;
        }

        int NewSelection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return selectedIndex > 0 ? selectedIndex - 1 : selectedIndex;
                case Direction.Down:
                    return selectedIndex < list.Count - 1 ? selectedIndex + 1 : selectedIndex;
            }

            return selectedIndex;
        }

        public void HandleDirection(Direction direction)
        {
            int newIndex = NewSelection(direction);

            if (newIndex == selectedIndex)
                return;

            selectedIndex = newIndex;
            if (OnSelectionChanged != null)
                OnSelectionChanged(this, new SelectionEventArgs { NewSelection = selectedIndex });
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var pkmn in list)
                yield return pkmn.Name;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
