﻿using System;
using System.Collections.Generic;
using FakeItEasy;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;
using GameEngineTest.Graphics;
using GameEngineTest.TestUtils;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using PokemonShared.Gui;
using PokemonShared.Gui.Renderer.PokemonClassicRenderer;

namespace BattleModeTest.Components.GraphicComponents
{
    [TestFixture]
    public class HpLineTest : IGraphicComponentTest
    {


        private static HpLine CreateLine()
        {
            var line = new HpLine(new ClassicLineRenderer(A.Fake<ITexture2D>(), A.Fake<ITexture2D>(), Color.White)) {MaxHp = 100};

            return line;
        }

        public static List<TestCaseData> HpTestData = new List<TestCaseData>
        {
            new TestCaseData(100),
            new TestCaseData(25),
            new TestCaseData(0)
        };

        public static List<TestCaseData> HpColorTestData = new List<TestCaseData>
        {
            new TestCaseData(100, Color.Green),
            new TestCaseData(50, Color.Green),
            new TestCaseData(49, Color.Yellow),
            new TestCaseData(25, Color.Yellow),
            new TestCaseData(24, Color.Red)
        };

        [TestCaseSource(nameof(HpColorTestData))]
        public void Draw_SetNumberOfHp_HPLineHasExpectedColor(int hp, Color color)
        {
            var line = CreateLine();
            line.SetCoordinates(0, 0, 500, 500);

            line.Current = hp;
            line.Draw();

            Assert.AreEqual(color, line.Color);
        }

        [TestCase]
        public void Draw_ZeroMaxHP_DoesNotThrow()
        {
            var line = CreateLine();
            line.SetCoordinates(0, 0, 500, 500);

            line.MaxHp = 0;
            line.Current = 0;

            Assert.DoesNotThrow(()=> line.Draw());
        }

        [TestCase(110)]
        [TestCase(-10)]
        public void Current_SetOutOfBound_ThrowsException(int hp)
        {
            var line = CreateLine();

            Assert.Throws<ArgumentOutOfRangeException>(() => line.Current = hp);
        }

        [TestCase(100, 90, 90)]
        [TestCase(80, 70, 70)]
        [TestCase(20, 0, 0)]
        public void Current_LowerMaxHp_IsChangedToFitInRange(int initalHp, int maxHp, int resultHp)
        {
            var line = CreateLine();
            line.Current = initalHp;
            line.MaxHp = maxHp;

            Assert.AreEqual(resultHp, line.Current);
        }

        [TestCase(-10)]
        public void MaxHP_SetOutOfBound_ThrowsException(int hp)
        {
            var line = CreateLine();

            Assert.Throws<ArgumentOutOfRangeException>(() => line.MaxHp = hp);
        }

        protected override IGuiComponent CreateComponent()
        {
            return CreateLine();
        }
    }
}
