using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleLib.Components;
using Moq;
using BattleLib.Components.Menu;

namespace BattleLibTest.Components.MenuComponent
{
    /*
    [TestFixture]
    public class MenuComponentTest
    {
        BattleLib.Components.Menu.MenuComponent component;

        [SetUp]
        public void init()
        {
            component = new BattleLib.Components.Menu.MenuComponent();
        }

        [TestCase]
        public void AddAndSetMenuTest()
        {
            Mock<IMenuModel> modelMock = new Mock<IMenuModel>();
            modelMock.SetupGet(m => m.Type).Returns(MenuType.Main);

            Assert.DoesNotThrow(() => component.AddModel(modelMock.Object));
            Assert.DoesNotThrow(() => component.SetMenu(MenuType.Main));

            Assert.DoesNotThrow(() => component.SetMenu(MenuType.None));
        }

        [TestCase]
        public void SetMenuExceptionTest()
        {
            bool eventFired = false; ;
            component.OnMenuChanged += (a, b) => { eventFired = true; };

            Assert.Throws<InvalidOperationException>(() => component.SetMenu(MenuType.Main));
            Assert.False(eventFired);

            Assert.DoesNotThrow(() => component.SetMenu(MenuType.None));
            Assert.False(eventFired);
        }

        [TestCase]
        public void MenuChangeEvent()
        {
            bool eventFired = false; ;
            component.OnMenuChanged += (a, b) => { eventFired = true; };
            Mock<IMenuModel> modelMock = new Mock<IMenuModel>();
            modelMock.SetupGet(m => m.Type).Returns(MenuType.Main);

            Assert.DoesNotThrow(() => component.AddModel(modelMock.Object));

            Assert.DoesNotThrow(() => component.SetMenu(MenuType.Main));
            Assert.True(eventFired);

            eventFired = false;
            Assert.DoesNotThrow(() => component.SetMenu(MenuType.None));
            Assert.True(eventFired);

            eventFired = false;
            Assert.Throws<InvalidOperationException>(() => component.SetMenu(MenuType.PKMN));
            Assert.False(false);

            eventFired = false;
            Assert.DoesNotThrow(() => component.SetMenu(MenuType.None));
            Assert.True(true);
        }

        [TestCase]
        public void InitalStateTest()
        {
            bool eventFired = false;;
            component.OnMenuChanged += (a, b) => { eventFired = true; };

            //After startup MenuComponent contains MenuType.None and has
            //it as current state
            Assert.DoesNotThrow(() => component.SetMenu(MenuType.None));
            Assert.False(eventFired);

            Assert.Throws<InvalidOperationException>(() => component.SetMenu(MenuType.Main));
            Assert.Throws<InvalidOperationException>(() => component.SetMenu(MenuType.Attack));
            Assert.Throws<InvalidOperationException>(() => component.SetMenu(MenuType.Item));
            Assert.Throws<InvalidOperationException>(() => component.SetMenu(MenuType.PKMN));

            Assert.False(eventFired);
        }

        [TestCase]
        public void InputHandlersTest()
        {

            Direction outputDir = Direction.Up;
            Mock<IMenuModel> modelMock = new Mock<IMenuModel>();
            modelMock.SetupGet(m => m.Type).Returns(MenuType.Main);
            modelMock.Setup(m => m.HandleDirection(It.IsAny<Direction>())).Callback((Direction dir) => outputDir = dir);

            component.AddModel(modelMock.Object);
            component.SetMenu(MenuType.Main);

            foreach(var dir in Enum.GetValues(typeof(Direction)).Cast<Direction>()){
                component.HandleDirection(dir);
                Assert.AreEqual(dir, outputDir);

            }
        }
    }
    */
}
