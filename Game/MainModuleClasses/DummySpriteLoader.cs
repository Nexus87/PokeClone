﻿using System.Diagnostics.Eventing.Reader;
using GameEngine;
using GameEngine.Registry;
using MainModule;
using MainModule.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PokemonGame.MainModuleClasses
{
    [GameService(typeof(ISpriteLoader))]
    public class DummySpriteLoader : ISpriteLoader
    {
        private readonly SpriteSheetFactory factory;

        public DummySpriteLoader(ContentManager contentManager)
        {
            var provider = new JsonSpriteSheetProvider("Characters Overworld", "Content/CharactersMapping.json", contentManager);
            factory = new SpriteSheetFactory(provider);
        }
        public CharacterSprite GetSprite(string spriteName)
        {
            var left = factory.CreateSpriteSheetTexture(spriteName + "Left");
            var right = factory.CreateSpriteSheetTexture(spriteName + "Left");
            var up = factory.CreateSpriteSheetTexture(spriteName + "Up");
            var down = factory.CreateSpriteSheetTexture(spriteName + "Down");

            right.SpriteEffects = SpriteEffects.FlipVertically;
            return new CharacterSprite(left, right, up, down);
        }

        public void Setup()
        {
            factory.Setup();
        }
    }
}