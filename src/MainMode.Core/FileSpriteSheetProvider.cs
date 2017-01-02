using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameEngine.Globals;
using Microsoft.Xna.Framework;

namespace MainMode.Core
{
    public class FileSpriteSheetProvider
    {
        private const int KeyIndex = 0;
        private const int RowIndex = 1;
        private const int ColumnIndex = 2;

        private readonly string _fileName;
        private Dictionary<string, Rectangle> _dictionary;

        public FileSpriteSheetProvider(string fileName)
        {
            this._fileName = fileName;
        }


        public Dictionary<string, Rectangle> GetMapping()
        {
            return _dictionary;
        }

        private IDictionary<string, TableIndex> ReadTableMapping()
        {
            var tmpList = new List<string>();
            using (var stream = new StreamReader(_fileName))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                    tmpList.Add(line);
            }

            var table = new Dictionary<string, TableIndex>();
            foreach (var line in tmpList)
            {
                var splitted = line.Split(';');
                var key = splitted[KeyIndex];
                var value = new TableIndex(int.Parse(splitted[RowIndex]), int.Parse(splitted[ColumnIndex]));
                table.Add(key, value);
            }

            return table;
        }

        public void Setup()
        {
            var table = ReadTableMapping();
            var tableRows = table.Values.Max(t => t.Row);
            var tableColumns = table.Values.Max(t => t.Column);
            var iconWidth = 608 / (float) tableColumns;
            var iconHeight = 656 / (float) tableRows;

            _dictionary = table
                .ToDictionary(v => v.Key, v => TableIndexToRectangle(v.Value, iconWidth, iconHeight));

        }

        private static Rectangle TableIndexToRectangle(TableIndex index, float iconWidth, float iconHeight)
        {
            var location = new Point((int) (index.Column * iconWidth), (int) (index.Row * iconHeight));
            var size = new Point((int) iconWidth, (int) iconHeight);

            return new Rectangle(location, size);
        }
    }
}