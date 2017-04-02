using GameEngine.Graphics.General;
using GameEngine.GUI;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;
using PokemonShared.Gui.Renderer;
using PokemonShared.Models;

namespace PokemonShared.Gui
{
    [GameType]
    public class HpText : AbstractGuiComponent
    {
        private readonly HpTextRenderer _renderer;

        public HpText(HpTextRenderer renderer)
        {
            _renderer = renderer;
        }

        public float PreferredTextHeight { get; set; }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _renderer.Render(batch, this);
        }


        public void SetPokemon(Pokemon pokemon)
        {
            MaxHp = pokemon.MaxHp;
            CurrentHp = pokemon.Hp;
        }

        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }

        protected override void Update()
        {
            PreferredHeight = _renderer.GetPreferredHeight(this);
            PreferredWidth = _renderer.GetPreferredWidth(this);
        }
    }
}