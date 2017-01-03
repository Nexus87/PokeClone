using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FakeItEasy;
using GameEngine.Globals;
using GameEngine.GUI.Controlls;
using NUnit.Framework;

namespace GameEngine.GUI.Test.Controls
{
    [TestFixture]
    public class ListViewTest
    {
        private const int InitialCallTimes = 2;

        private class FakeComponent : AbstractGuiComponent
        {
            public int Value { get; set; }
        }

        [TestCase(10)]
        public void Update_WithModel_AllItemsWereUpdated(int rows)
        {
            var model = CreateModel(rows);
            var components = CreateComponentMockList(rows);
            var listView = CreateListViewWithCellFactory(r => components[r]);

            listView.Model = model;
            listView.Draw();

            foreach (var mock in components)
            {
                A.CallToSet(() => mock.Area).MustHaveHappened(Repeated.AtLeast.Once);
            }
        }

        [TestCase(10)]
        public void Update_RemoveComponentFromModel_ChangeIsReflected(int rows)
        {
            var model = CreateModel(rows);
            var components = CreateComponentMockList(rows);
            var listView = CreateListViewWithCellFactory(r => components[r]);

            listView.Model = model;
            listView.Draw();

            model.Remove(model.Last());
            listView.Draw();

            A.CallToSet(() => components.Last().Area).MustHaveHappened(Repeated.Exactly.Times(InitialCallTimes));
        }

        [TestCase(10)]
        public void Update_AfterInsertData_NewRowIsResized(int rows)
        {
            var model = CreateModel(rows);
            var listView = CreateListViewWithCellFactory(r => new FakeComponent{Value = r});

            model.Remove(5);
            listView.Model = model;
            listView.Draw();

            model.Insert(5, 5);
            listView.Draw();

            var newComponent = listView.GetComponent(5) as FakeComponent;
            Assert.AreEqual(5, newComponent.Value);
        }

        [TestCase(10)]
        public void Update_AddComponentToModel_NewComponentIsResized(int rows)
        {
            var model = CreateModel(rows);
            var components = CreateComponentMockList(rows + 1);
            var listView = CreateListViewWithCellFactory(r => components[r]);

            listView.Model = model;
            listView.Draw();

            model.Add(rows);
            listView.Draw();

            A.CallToSet(() => components.Last().Area).MustHaveHappened(Repeated.AtLeast.Once);

        }

        [TestCase]
        public void HandleKeyInput_WithNotSelectableComponent_ComponentIsNotSelected()
        {
            var model = CreateModel(2);
            var components = CreateComponentMockList(2);
            A.CallTo(() => components[1].IsSelectable).Returns(false);
            A.CallTo(() => components[0].IsSelectable).Returns(true);
            var listView = CreateListViewWithCellFactory(r => components[r]);
            listView.Model = model;
            listView.SelectCell(0);

            listView.HandleKeyInput(CommandKeys.Down);

            Assert.True(components[0].IsSelected);
            Assert.False(components[1].IsSelected);
        }

        [TestCase(0, CommandKeys.Down, 1, 0)]
        [TestCase(0, CommandKeys.Up, 0, 1)]
        [TestCase(1, CommandKeys.Up, 0, 1)]
        [TestCase(1, CommandKeys.Down, 1, 0)]
        public void HandleKeyInput_WithBothComponentSelected_ComponentIsNotSelected(
            int startIndex, CommandKeys key, int expectedSelectedIndex, int expectedNotSelectedIndex)
        {
            var model = CreateModel(2);
            var components = CreateComponentMockList(2);
            A.CallTo(() => components[0].IsSelectable).Returns(true);
            A.CallTo(() => components[1].IsSelectable).Returns(true);
            var listView = CreateListViewWithCellFactory(r => components[r]);

            listView.Model = model;
            listView.SelectCell(startIndex);
            components[startIndex].ComponentSelected +=
                Raise.With(new ComponentSelectedEventArgs {Source = components[startIndex]});

            listView.HandleKeyInput(key);

            Assert.True(components[expectedSelectedIndex].IsSelected);
            Assert.False(components[expectedNotSelectedIndex].IsSelected);
        }
        private static ListView<int> CreateListViewWithCellFactory(ListCellFactory<int> factory)
        {
            return new ListView<int>() {ListCellFactory = factory};
        }

        private static List<IGuiComponent>  CreateComponentMockList(int rows)
        {
            return Enumerable
                .Range(0, rows)
                .Select(i => A.Fake<IGuiComponent>())
                .ToList();
        }

        private static ObservableCollection<int> CreateModel(int rows)
        {
            return new ObservableCollection<int>(Enumerable.Range(0, rows));
        }
    }
}