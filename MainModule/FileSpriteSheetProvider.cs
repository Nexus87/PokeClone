using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MainModule
{
    public class FileSpriteSheetProvider : ISpriteSheetProvider
    {
        private const int KeyIndex = 0;
        private const int RowIndex = 1;
        private const int ColumnIndex = 2;

        private readonly string textureName;
        private readonly string fileName;
        private readonly ContentManager contentManager;

        public FileSpriteSheetProvider(string textureName, string fileName, ContentManager contentManager)
        {
            this.textureName = textureName;
            this.fileName = fileName;
            this.contentManager = contentManager;
        }

        public ITexture2D GetTexture()
        {
            return new XNATexture2D(contentManager.Load<Texture2D>(textureName));
        }

        public IDictionary<string, TableIndex> GetMapping()
        {
            var tmpList = new List<string>();
            using (var stream = new StreamReader(fileName))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                    tmpList.Add(line);
            }

            var dictionary = new Dictionary<string, TableIndex>();
            foreach (var line in tmpList)
            {
                var splitted = line.Split(';');
                var key = splitted[KeyIndex];
                var value = new TableIndex(int.Parse(splitted[RowIndex]), int.Parse(splitted[ColumnIndex]));
                dictionary.Add(key, value);
            }

            return dictionary;
        }
    }
}