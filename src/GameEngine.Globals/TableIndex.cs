﻿namespace GameEngine.Globals
{
    public struct TableIndex
    {
        public TableIndex(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row;
        public int Column;
    }
}