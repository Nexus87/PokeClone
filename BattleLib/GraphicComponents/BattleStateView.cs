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
    public class BattleStateView : AbstractGraphicComponent
    {

        int lastPlayerId;
        int lastAIId;

        StatusCondition lastPlayerCondition;
        StatusCondition lastAICondition;

        float lastPlayerHP;
        float lastAIHP;

        BattleModel model;
        TextureProvider provider;

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
                lastPlayerId = model.PlayerPkmn;
            if (lastAIId != model.AIPkmn)
                lastAIId = model.AIPkmn;
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
            throw new NotImplementedException();
        }

        public override void Draw(GameTime time, SpriteBatch batch, int screenWidth, int screenHeight)
        {
            throw new NotImplementedException();
        }
    }
}
