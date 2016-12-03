using Castle.Core.Internal;
using FakeItEasy;
using GameEngine.Graphics.General;
using GameEngine.GUI.Builder;
using GameEngine.GUI.Builder.Controls;
using GameEngine.GUI.Builder.Panels;
using GameEngine.GUI.ComponentRegistry;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Panels;
using GameEngine.GUI.Renderers;
using GameEngine.GUI.Renderers.PokemonClassicRenderer;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace GameEngine.GUI.Test
{
    [TestFixture]
    public class IntegrationTests
    {
        private readonly GuiComponentRegistry _registry = new GuiComponentRegistry();
        private IBuilderFactory _builderFactory;

        [TestCase(@"\Resources\BuildFromXml.xml")]
        public void BuildGuiFromXml(string filePath)
        {
            RegisterGraphicComponents();
            var builder = new GraphicComponentLoader(_builderFactory){FilePath = TestDirectory + filePath};
            var component = builder.Load();

            Assert.IsInstanceOf<Grid>(component);

            var grid = (Grid) component;
            var gridArea = new Rectangle(100, 200, 700, 500);

            Assert.AreEqual(gridArea, grid.Constraints);

            Assert.AreEqual(2, grid.Rows);
            Assert.AreEqual(2, grid.Columns);

            grid.Children.ForEach(Assert.IsInstanceOf<Button>);

            Assert.AreEqual("Button1", ((Button) grid.GetComponent(0, 0)).Text);
            Assert.AreEqual("Button2", ((Button) grid.GetComponent(1, 0)).Text);
            Assert.AreEqual("Button3", ((Button) grid.GetComponent(0, 1)).Text);
            Assert.AreEqual("Button4", ((Button) grid.GetComponent(1, 1)).Text);
        }


        private static string TestDirectory => TestContext.CurrentContext.TestDirectory;

        public class SkinFake : ISkin
        {
            public IButtonRenderer BuildButtonRenderer()
            {

                return new ClassicButtonRenderer(A.Fake<ITexture2D>(), A.Fake<ISpriteFont>());
            }

            public float DefaultTextHeight { get; } = 16;
        }

        private void RegisterGraphicComponents()
        {
            _registry.RegisterGuiComponent<Button, ButtonBuilder>();
            _registry.RegisterGuiComponent<Grid, GridBuilder>();
            _registry.RegisterBuilderFactory<BuilderFactory>();
            _registry.RegisterSkin<SkinFake>();
            _registry.Init();
            _builderFactory = _registry.GetBuilderFactory();
        }
    }
}