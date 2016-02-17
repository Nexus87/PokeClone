using BattleLib.Components.BattleState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.GraphicComponents
{
    public interface IBattleGraphicService
    {
        event EventHandler OnHPSet;
        event EventHandler OnPokemonSet;

        void SetPlayerHP(int HP);
        void SetAIHP(int HP);

        void SetPlayerPKMN(PokemonWrapper pokemon);
        void SetAIPKMN(PokemonWrapper pokemon);
    }
}
