using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameEngine.Core;
using GameEngine.Globals;
using GameEngine.GUI.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MainMode.Core
{
    public class FileSpriteSheetProvider : ISpriteSheetProvider
    {
        private const int KeyIndex = 0;
        private const int RowIndex = 1;
        private const int ColumnIndex = 2;

        private readonly string fileName;
        private readonly ITexture2D texture;
        private Dictionary<string, Rectangle> dictionary;

        public FileSpriteSheetProvider(string textureName, string fileName, ContentManager contentManager)
        {
            this.fileName = fileName;
            texture = new XnaTexture2D(textureName, contentManager);
        }

        public ITexture2D GetTexture()
        {
            return texture;
        }

        public IDictionary<string, Rectangle> GetMapping()
        {
            return dictionary;
        }

        private IDictionary<string, TableIndex> ReadTableMapping()
        {
            var tmpList = new List<string>();
            using (var stream = new StreamReader(fileName))
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
            texture.LoadContent();
            var table = ReadTableMapping();
            var tableRows = table.Values.Max(t => t.Row);
            var tableColumns = table.Values.Max(t => t.Column);
            var iconWidth = texture.Width / (float) tableColumns;
            var iconHeight = texture.Height / (float) tableRows;

            dictionary = table
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