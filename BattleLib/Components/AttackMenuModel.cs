using Base;
using System;
using System.Collections.Generic;

namespace BattleLib.Components
{
    public class AttackMenuModel : IMenuModel
    {
        public List<Move> Moves { get; private set; }

        int selectedIndex = 0;
        public AttackMenuModel(Pokemon pkm)
        {
            Moves = pkm.Moves;
        }

        public MenuType Type{ get { return MenuType.Attack; } }
        
        public event EventHandler<SelectionEventArgs> OnSelectionChanged;

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
                    return selectedIndex < Moves.Count - 1 ? selectedIndex + 1 : selectedIndex;
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

        public MenuType Select()
        {
            throw new NotImplementedException();
        }
    }
}