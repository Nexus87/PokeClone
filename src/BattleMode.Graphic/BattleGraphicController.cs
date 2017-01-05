using System;
using System.Collections.Generic;
using System.Linq;
using Base.Data;
using BattleMode.Core.Components.GraphicComponents;
using BattleMode.Entities.BattleState;
using BattleMode.Gui;
using BattleMode.Shared;
using GameEngine.Core;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace BattleMode.Graphic
{
    [GameService(typeof(IBattleGraphicController))]
    public class BattleGraphicController : AbstractGuiComponent, IBattleGraphicController
    {
        private readonly Dictionary<ClientIdentifier, IPokemonDataView> _dataViews = new Dictionary<ClientIdentifier, IPokemonDataView>();
        private readonly Dictionary<ClientIdentifier, PokemonSprite> _sprites = new Dictionary<ClientIdentifier, PokemonSprite>();

        public BattleGraphicController(ScreenConstants screen, 
            PlayerPokemonDataView playerView, AiPokemonDataView aiView, 
            PokemonSprite playerSprite, PokemonSprite aiSprite,
            BattleData data)
        {
            var player = data.PlayerId;
            var ai = data.Clients.First(id => !id.IsPlayer);

            playerSprite.IsPlayer = true;
            aiSprite.IsPlayer = false;

            _dataViews[player] = playerView;
            _dataViews[ai] = aiView;

            _sprites[player] = playerSprite;
            _sprites[ai] = aiSprite;

            foreach (var sprite in _sprites.Values)
                sprite.OnPokemonAppeared += delegate { PokemonSet?.Invoke(this, null); };

            foreach(var view in _dataViews.Values)
                view.Show();

            initAIGraphic(aiSprite, screen);
            initPlayerGraphic(playerSprite, screen);
        }

        public event EventHandler ConditionSet
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        public event EventHandler HpSet;
        public event EventHandler PokemonSet;

        public void PlayAttackAnimation(ClientIdentifier id)
        {
            _sprites[id].PlayAttackAnimation();
        }

        public void SetHp(ClientIdentifier id, int value)
        {
            _dataViews[id].SetHp(value);
            HpSet?.Invoke(this, EventArgs.Empty);
        }

        public void SetPokemon(ClientIdentifier id, PokemonWrapper pokemon)
        {
            _dataViews[id].SetPokemon(pokemon);
            _sprites[id].SetPokemon(pokemon.ID);
        }

        public void SetPokemonStatus(ClientIdentifier id, StatusCondition condition)
        {
            throw new NotImplementedException();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            foreach (var sprite in _sprites.Values)
                sprite.Draw(time, batch);
        }

        private void initAIGraphic(PokemonSprite aiSprite, ScreenConstants screen)
        {
            var xPosition = screen.ScreenWidth * 0.6f;
            var yPosition = screen.ScreenHeight * 0.1f;

            var height = screen.ScreenHeight * 0.25f;
            var width = screen.ScreenHeight * 0.25f;

            aiSprite.SetCoordinates(xPosition, yPosition, width, height);
        }

        private void initPlayerGraphic(PokemonSprite playerSprite, ScreenConstants screen)
        {
            var xPosition = screen.ScreenWidth * 0.2f;
            var yPosition = screen.ScreenHeight * 0.4f;

            var height = screen.ScreenHeight * 0.25f;
            var width = screen.ScreenHeight * 0.25f;

            playerSprite.SetCoordinates(xPosition, yPosition, width, height);
        }
    }
}