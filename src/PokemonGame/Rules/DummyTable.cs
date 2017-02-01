using Base.Data;
using Base.Rules;
using System;
using GameEngine.TypeRegistry;

namespace PokemonGame.Rules
{
    [GameService(typeof(ITypeTable))]
    public class DummyTable : ITypeTable
    {
        private readonly int _typeCount = Enum.GetNames(typeof(PokemonType)).Length;

        private readonly float[,] _table;

        public DummyTable()
        {
            _table = new float[_typeCount, _typeCount];
            InitTable();
        }

        public float GetModifier(PokemonType source, PokemonType target)
        {
            return _table[(int)source, (int)target];
        }

        private void InitTable()
        {
            for (int i = 0; i < _typeCount; i++)
            {
                for (int j = 0; j < _typeCount; j++)
                    _table[i, j] = 1.0f;
            }
        }
    }
}