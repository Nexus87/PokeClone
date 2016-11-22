﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GameEngine.GUI.Controlls;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;

namespace GameEngine.GUI.Test.Controls
{
    [TestFixture]
    public class ListViewTest
    {
        [TestCase(10)]
        public void Update_WithModel_AllItemsWereUpdated(int rows)
        {
            var model = CreateModel(rows);
            var components = CreateComponentMockList(rows);
            var listView = CreateListViewWithCellFactory(r => components[r].Object);

            listView.Model = model;
            listView.Update(new GameTime());

            foreach (var mock in components)
            {
                mock.VerifySet(c => c.Constraints = It.IsAny<Rectangle>(), Times.AtLeastOnce);
            }
        }

        [TestCase(10)]
        public void Update_RemoveComponentFromModel_ChangeIsReflected(int rows)
        {
            var model = CreateModel(rows);
            var components = CreateComponentMockList(rows);
            var listView = CreateListViewWithCellFactory(r => components[r].Object);

            listView.Model = model;
            listView.Update(new GameTime());
            ResetComponents(components);

            model.Remove(model.Last());
            listView.Update(new GameTime());

            components.Last().VerifySet(c => c.Constraints = It.IsAny<Rectangle>(), Times.Never);
        }

        [TestCase(10)]
        public void Update_AddComponentToModel_NewComponentIsResized(int rows)
        {
            var model = CreateModel(rows);
            var components = CreateComponentMockList(rows + 1);
            var listView = CreateListViewWithCellFactory(r => components[r].Object);

            listView.Model = model;
            listView.Update(new GameTime());
            ResetComponents(components);

            model.Add((rows + 1) + "");
            listView.Update(new GameTime());

            components.Last().VerifySet(c => c.Constraints = It.IsAny<Rectangle>(), Times.AtLeastOnce);

        }

        private void ResetComponents(List<Mock<IListCell>> components)
        {
            components.ForEach(c => c.ResetCalls());
        }

        private static ListView<string> CreateListViewWithCellFactory(ListCellFactory factory)
        {
            return new ListView<string> {ListCellFactory = factory};
        }

        private static List<Mock<IListCell>>  CreateComponentMockList(int rows)
        {
            return Enumerable
                .Range(0, rows)
                .Select(i => new Mock<IListCell>())
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