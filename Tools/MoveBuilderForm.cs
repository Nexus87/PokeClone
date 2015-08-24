using Base;
using Eto.Drawing;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class MoveBuilderForm : Panel
    {
        IEnumerable<Object> _types = Enum.GetValues(typeof(PokemonType)).Cast<Object>();

        public MoveBuilderForm()
        {
            init();
        }

        private TableRow dataRow()
        {
            var box = new GroupBox
            {
                Text = "Data",
                Content = new TableLayout
                {
                    Rows =
                    {
                        new TableRow(
                            new Label{ Text = "Damage: "},
                            new TextBox{PlaceholderText = "0"},
                            new Label{ Text = "Accuracy: "},
                            new TextBox{PlaceholderText = "0"}
                        ),
                        new TableRow(
                            new Label{Text = "PP: "},
                            new TextBox{PlaceholderText = "0"},
                            new Label{Text = "Type: "},
                            new ComboBox{ DataStore = _types, SelectedIndex = (int) PokemonType.Normal}
                        ),
                        new TableRow()
                    }
                }
            };

            return new TableRow(box, new TableCell(null, true));
        }

        private TableRow nameRow()
        {
            var row = new TableRow(
                new TableLayout
                {
                    Rows =
                    {
                        new TableRow(
                            new Label{ Text = "Name: "},
                            new TextBox{PlaceholderText = "Please insert a name"}
                        )
                    }
                }
            );

            return row;
        }

        private void init()
        {
            var dataLayout = new TableLayout
            {
                Padding = new Padding(5, 5),
                Spacing = new Size(5, 5),
                Rows =
                {
                    nameRow(),
                    dataRow(),
                    new TableRow()
                }
            };


            this.Content = new TableLayout
            {
                Padding = new Padding(5, 5),
                Spacing = new Size(5, 5),
                Rows =
                {
                    new TableRow(dataLayout, new GroupBox{ Text = "Moves", Content = new ListBox()}, new TableCell(null, true)),
                    new TableRow()
                }
            };
        }
    }
}
