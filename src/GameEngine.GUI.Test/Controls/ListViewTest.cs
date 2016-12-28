using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FakeItEasy;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Panels;
using NUnit.Framework;

namespace GameEngine.GUI.Test.Controls
{
    [TestFixture]
    public class ListViewTest
    {
        private const int InitialCallTimes = 1;

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
        public void Update_AddComponentToModel_NewComponentIsResized(int rows)
        {
            var model = CreateModel(rows);
            var components = CreateComponentMockList(rows + 1);
            var listView = CreateListViewWithCellFactory(r => components[r]);

            listView.Model = model;
            listView.Draw();

            model.Add(rows + 1);
            listView.Draw();

            A.CallToSet(() => components.Last().Area).MustHaveHappened(Repeated.AtLeast.Once);

        }

        private static ListView<int> CreateListViewWithCellFactory(ListCellFactory<int> factory)
        {
            return new ListView<int>(new Grid()) {ListCellFactory = factory};
        }

        private static List<IListCell>  CreateComponentMockList(int rows)
        {
            return Enumerable
                .Range(0, rows)
                .Select(i => A.Fake<IListCell>())
                .ToList();
        }

        private static ObservableCollection<int> CreateModel(int rows)
        {
            return new ObservableCollection<int>(Enumerable.Range(0, rows));
        }
    }
}