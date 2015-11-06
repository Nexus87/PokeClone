using Base;
using BattleLib.Components.BattleState;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.GraphicComponents
{
    public class BattleStateView : AbstractGraphicComponentOld
    {

        int lastPlayerId;
        int lastAIId;

        StatusCondition lastPlayerCondition;
        StatusCondition lastAICondition;

        float lastPlayerHP;
        float lastAIHP;

        BattleModel model;
        TextureProvider provider;

        Texture2D playerTexture;
        Texture2D aiTexture;

        public BattleStateView(BattleModel model, TextureProvider provider)
        {
            this.model = model;
            this.provider = provider;

            model.OnDataChange += model_OnDataChange;
            model_OnDataChange(null, null);
        }

        void model_OnDataChange(object sender, object e)
        {
            if (lastPlayerId != model.PlayerPkmn)
                UpdatePlayerId();
            if (lastAIId != model.AIPkmn)
                UpdteAiId();

            if (lastPlayerCondition != model.PlayerCondition)
                lastPlayerCondition = model.PlayerCondition;
            if (lastAICondition != model.AICondition)
                lastAICondition = model.AICondition;
            if (lastPlayerHP.CompareTo(model.PlayerHP) != 0)
                lastPlayerHP = model.PlayerHP;
            if (lastAIHP.CompareTo(model.AIHP) != 0)
                lastAIHP = model.AIHP;
        }

        public override void Setup(Rectangle screen, ContentManager content)
        {
            UpdatePlayerId();
            UpdteAiId();
        }

        private void UpdteAiId()
        {
            lastAIId = model.AIPkmn;
            aiTexture = provider.getTexturesFront(lastAIId);
        }

        private void UpdatePlayerId()
        {
            lastPlayerId = model.PlayerPkmn;
            playerTexture = provider.getTextureBack(lastPlayerId);
        }

        public override void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeight)
        {
            throw new NotImplementedException();
        }
    }
}
