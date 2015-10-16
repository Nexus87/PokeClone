using Base;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BattleLib.Components
{
    public class AttackMenuModel : IMenuModel
    {
        List<Move> moves;

        int selectedIndex = 0;
        public AttackMenuModel(Pokemon pkm)
        {
            moves = pkm.Moves;
        }

        //TODO remove this constructor
        public AttackMenuModel()
        {
            MoveData data1 = new MoveData{Name = "Attack1"};
            MoveData data2 = new MoveData{Name = "Attack2"};
            moves = new List<Move> { new Move(data1), new Move(data2) };
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
                    return selectedIndex < moves.Count - 1 ? selectedIndex + 1 : selectedIndex;
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

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var move in moves)
                yield return move.Data.Name;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}