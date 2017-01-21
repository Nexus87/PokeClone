using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameEngine.Graphics.Textures;
using GameEngine.TypeRegistry;
using MainMode.Globals;
using MainMode.Graphic;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace MainMode.Core.Loader
{
    public class EntityJson
    {
        public string Name { get; set; }
        public Dictionary<string, string> Mappings { get; set; } = new Dictionary<string, string>();
    }

    [GameService(typeof(CharacterSpriteLoader))]
    public class CharacterSpriteLoader
    {
        private readonly TextureProvider _textureProvider;
        private Dictionary<string, SpriteEntity> _entityMapping;
        private readonly Dictionary<string, SpriteEntityTextures> _cachedTextures = new Dictionary<string, SpriteEntityTextures>();

        public CharacterSpriteLoader(TextureProvider textureProvider)
        {
            _textureProvider = textureProvider;
        }

        public void LoadEntityMapping(string path)
        {
            _entityMapping = JsonConvert.DeserializeObject<List<EntityJson>>(File.ReadAllText(@"Content\" + path))
                .ToDictionary(x => x.Name, ToSpriteEntity);
        }

        private SpriteEntity ToSpriteEntity(EntityJson entityJson)
        {
            var entity = new SpriteEntity();
            string tmp;

            entityJson.Mappings.TryGetValue(nameof(entity.Front), out tmp);
            entity.Front = tmp;
            entityJson.Mappings.TryGetValue(nameof(entity.Left), out tmp);
            entity.Left = tmp;
            entityJson.Mappings.TryGetValue(nameof(entity.Back), out tmp);
            entity.Back = tmp;
            entityJson.Mappings.TryGetValue(nameof(entity.FrontMoving), out tmp);
            entity.FrontMoving = tmp;
            entityJson.Mappings.TryGetValue(nameof(entity.LeftMoving), out tmp);
            entity.LeftMoving = tmp;
            entityJson.Mappings.TryGetValue(nameof(entity.BackMoving), out tmp);
            entity.BackMoving = tmp;

            return entity;
        }

        public SpriteEntityTextures GetSpriteTextures(string name)
        {
            SpriteEntityTextures textures;
            if (!_cachedTextures.TryGetValue(name, out textures))
            {
                textures = CreateTexture(name);
                _cachedTextures[name] = textures;
            }

            return textures;
        }

        private SpriteEntityTextures CreateTexture(string name)
        {
            var mapping = _entityMapping[name];
            var getTexture = new Func<string, ITexture2D>(texture => _textureProvider.GetTexture(TextureKey.Key, texture));

            var standingTextures = new Dictionary<Direction, Tuple<ITexture2D, SpriteEffects>>();
            var movingTextures = new Dictionary<Direction, Tuple<ITexture2D, SpriteEffects>>();

            if (mapping.Front != null)
            {
                standingTextures[Direction.Down] = Tuple.Create(getTexture(mapping.Front), SpriteEffects.None);
            }
            if (mapping.Left != null)
            {
                standingTextures[Direction.Left] = Tuple.Create(getTexture(mapping.Left), SpriteEffects.None);
            }

            if (mapping.Right != null)
            {
                standingTextures[Direction.Right] = Tuple.Create(getTexture(mapping.Right), SpriteEffects.None);
            }
            else if (mapping.Left != null)
            {
                standingTextures[Direction.Right] = Tuple.Create(getTexture(mapping.Left), SpriteEffects.FlipHorizontally);
            }

            if (mapping.Back != null)
            {
                standingTextures[Direction.Up] = Tuple.Create(getTexture(mapping.Back), SpriteEffects.None);
            }


            if (mapping.FrontMoving != null)
            {
                movingTextures[Direction.Down] = Tuple.Create(getTexture(mapping.FrontMoving), SpriteEffects.None);
            }
            if (mapping.LeftMoving != null)
            {
                movingTextures[Direction.Left] = Tuple.Create(getTexture(mapping.LeftMoving), SpriteEffects.None);
            }

            if (mapping.RightMoving != null)
            {
                movingTextures[Direction.Right] = Tuple.Create(getTexture(mapping.RightMoving), SpriteEffects.None);
            }
            else if (mapping.LeftMoving != null)
            {
                movingTextures[Direction.Right] = Tuple.Create(getTexture(mapping.LeftMoving), SpriteEffects.FlipHorizontally);
            }

            if (mapping.BackMoving != null)
            {
                movingTextures[Direction.Up] = Tuple.Create(getTexture(mapping.BackMoving), SpriteEffects.None);
            }

            return new SpriteEntityTextures {MovingTextures = movingTextures, StandingTextures = standingTextures};
        }
    }
}