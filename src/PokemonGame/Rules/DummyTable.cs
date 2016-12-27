using Base.Data;
using Base.Rules;
using System;
using GameEngine.TypeRegistry;

namespace PokemonGame.Rules
{
    [GameService(typeof(ITypeTable))]
    public class DummyTable : ITypeTable
    {
        private readonly int typeCount = Enum.GetNames(typeof(PokemonType)).Length;

        private float[,] table;

        public DummyTable()
        {
            table = new float[typeCount, typeCount];
            initTable();
        }

        public float GetModifier(PokemonType source, PokemonType target)
        {
            return table[(int)source, (int)target];
        }

        private void initTable()
        {
            for (int i = 0; i < typeCount; i++)
            {
                for (int j = 0; j < typeCount; j++)
                    table[i, j] = 1.0f;
            }
        }
    }
}