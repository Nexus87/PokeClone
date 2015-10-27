using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public class TypeTable
    {
        public TypeTable()
        {
            table = new float[typeCount, typeCount];
            initTable();
        }

        private void initTable()
        {
            for (int i = 0; i < typeCount; i++)
            {
                for (int j = 0; j < typeCount; j++)
                    table[i, j] = 1.0f;
            }


        }
        readonly int typeCount = Enum.GetNames(typeof(PokemonType)).Length;
        float[,] table;

        public float GetModifier(PokemonType source, PokemonType target)
        {
            return table[(int) source, (int) target];
        }
    }
}
