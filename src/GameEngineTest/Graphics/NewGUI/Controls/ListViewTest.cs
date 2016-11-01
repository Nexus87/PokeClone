using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GameEngine.Graphics.NewGUI.Controlls;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;

namespace GameEngineTest.Graphics.NewGUI.Controls
{
    [TestFixture]
    public class ListViewTest
    {
        [TestCase(10)]
        public void Update_WithModel_AllItemsWereUpdated(int rows)
        {
            var model = CreateModel(rows);
            var components = CreateComponentMockList(rows);
            var listView = CreateListView(r => components[r].Object);

            listView.Model = model;
            listView.Update(new GameTime());

            foreach (var mock in components)
            {
                mock.VerifySet(c => c.Constraints = It.IsAny<Rectangle>(), Times.AtLeastOnce);
            }
        }

        private static ListView<string> CreateListView(ListCellFactory factory)
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