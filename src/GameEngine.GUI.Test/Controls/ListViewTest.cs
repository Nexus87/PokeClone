using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FakeItEasy;
using GameEngine.GUI.Controlls;
using Microsoft.Xna.Framework;
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
            listView.Update(new GameTime());

            foreach (var mock in components)
            {
                A.CallToSet(() => mock.Constraints).MustHaveHappened(Repeated.AtLeast.Once);
            }
        }

        [TestCase(10)]
        public void Update_RemoveComponentFromModel_ChangeIsReflected(int rows)
        {
            var model = CreateModel(rows);
            var components = CreateComponentMockList(rows);
            var listView = CreateListViewWithCellFactory(r => components[r]);

            listView.Model = model;
            listView.Update(new GameTime());

            model.Remove(model.Last());
            listView.Update(new GameTime());

            A.CallToSet(() => components.Last().Constraints).MustHaveHappened(Repeated.Exactly.Times(InitialCallTimes));
        }

        [TestCase(10)]
        public void Update_AddComponentToModel_NewComponentIsResized(int rows)
        {
            var model = CreateModel(rows);
            var components = CreateComponentMockList(rows + 1);
            var listView = CreateListViewWithCellFactory(r => components[r]);

            listView.Model = model;
            listView.Update(new GameTime());

            model.Add((rows + 1) + "");
            listView.Update(new GameTime());

            A.CallToSet(() => components.Last().Constraints).MustHaveHappened(Repeated.AtLeast.Once);

        }

        private static ListView<string> CreateListViewWithCellFactory(ListCellFactory factory)
        {
            return new ListView<string> {ListCellFactory = factory};
        }

        private static List<IListCell>  CreateComponentMockList(int rows)
        {
            return Enumerable
                .Range(0, rows)
                .Select(i => A.Fake<IListCell>())
                .ToList();
        }

        private static ObservableCollection<string> CreateModel(int rows)
        {

            return new ObservableCollection<string>(
                    Enumerable
                        .Range(0, rows)
                        .Select(i => i+"")
            );
        }
    }
}