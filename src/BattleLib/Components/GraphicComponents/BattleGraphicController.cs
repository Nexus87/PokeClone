﻿using System;
using System.Collections.Generic;
using System.Linq;
using Base.Data;
using BattleLib.Components.BattleState;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using Microsoft.Xna.Framework;

namespace BattleLib.Components.GraphicComponents
{
    [GameService(typeof(IBattleGraphicController))]
    public class BattleGraphicController : AbstractGraphicComponent, IBattleGraphicController
    {
        private readonly Dictionary<ClientIdentifier, PokemonDataView> dataViews = new Dictionary<ClientIdentifier, PokemonDataView>();
        private readonly Dictionary<ClientIdentifier, PokemonSprite> sprites = new Dictionary<ClientIdentifier, PokemonSprite>();

        public BattleGraphicController(ScreenConstants screen, 
            PlayerPokemonDataView playerView, AIPokemonDataView aiView, 
            PokemonSprite playerSprite, PokemonSprite aiSprite,
            BattleData data)
        {
            var player = data.PlayerId;
            var ai = data.Clients.First(id => !id.IsPlayer);

            playerSprite.IsPlayer = true;
            aiSprite.IsPlayer = false;

            dataViews[player] = playerView;
            dataViews[ai] = aiView;

            sprites[player] = playerSprite;
            sprites[ai] = aiSprite;

            foreach (var view in dataViews.Values)
                view.OnHPUpdated += delegate { OnHPSet(this, null); };

            foreach (var sprite in sprites.Values)
                sprite.OnPokemonAppeared += delegate { OnPokemonSet(this, null); };

            initAIGraphic(aiView, aiSprite, screen);
            initPlayerGraphic(playerView, playerSprite, screen);
        }

        public event EventHandler ConditionSet
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        public event EventHandler OnHPSet;
        public event EventHandler OnPokemonSet;

        public void PlayAttackAnimation(ClientIdentifier id)
        {
            sprites[id].PlayAttackAnimation();
        }

        public void SetHP(ClientIdentifier id, int value)
        {
            dataViews[id].SetHP(value);
        }

        public void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon)
        {
            dataViews[id].SetPokemon(pokemon);
            sprites[id].SetPokemon(pokemon.ID);
        }

        public void SetPokemonStatus(ClientIdentifier id, StatusCondition condition)
        {
            throw new NotImplementedException();
        }

        public override void Setup()
        {
            foreach (var view in dataViews.Values)
                view.Setup();

            foreach (var sprite in sprites.Values)
                sprite.Setup();

        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            foreach (var view in dataViews.Values)
                view.Draw(time, batch);

            foreach (var sprite in sprites.Values)
                sprite.Draw(time, batch);
        }

        private void initAIGraphic(PokemonDataView aiView, PokemonSprite aiSprite, ScreenConstants screen)
        {
            aiView.XPosition = screen.ScreenWidth * 0.2f;
            aiView.YPosition = screen.ScreenHeight * 0.1f;

            aiView.Height = screen.ScreenHeight * 0.1f;
            aiView.Width = screen.ScreenWidth * 0.15f;

            aiSprite.XPosition = screen.ScreenWidth * 0.6f;
            aiSprite.YPosition = screen.ScreenHeight * 0.1f;

            aiSprite.Height = screen.ScreenHeight * 0.25f;
            aiSprite.Width = screen.ScreenHeight * 0.25f;
        }

        private void initPlayerGraphic(PokemonDataView playerView, PokemonSprite playerSprite, ScreenConstants screen)
        {
            playerView.XPosition = screen.ScreenWidth * 0.55f;
            playerView.YPosition = screen.ScreenHeight * 0.45f;

            playerView.Height = screen.ScreenHeight * 0.15f;
            playerView.Width = screen.ScreenWidth * 0.15f;

            playerSprite.XPosition = screen.ScreenWidth * 0.2f;
            playerSprite.YPosition = screen.ScreenHeight * 0.4f;

            playerSprite.Height = screen.ScreenHeight * 0.25f;
            playerSprite.Width = screen.ScreenHeight * 0.25f;
        }
    }
}