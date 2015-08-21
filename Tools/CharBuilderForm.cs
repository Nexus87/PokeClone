using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Eto.Forms;
using Eto.Drawing;
using Base;
namespace Tools
{
    public class CharBuilderForm : Panel
    {

        TextBox _name;
        TextBox _hp;
        TextBox _atk;
        TextBox _def;
        TextBox _spAtk;
        TextBox _spDef;
        TextBox _speed;

        List<PKData> _list = new List<PKData>();
        IEnumerable<Object> _types = Enum.GetValues(typeof(PokemonType)).Cast<Object>();

        public CharBuilderForm()
        {

            init();
        }

        void init()
        {
            var FieldsLayout = new TableLayout
            {
                Padding = new Padding(5),
                Spacing = new Size(5, 5),
                Rows =
                {
                    new TableRow(
                        new TableCell(new Label{ Text = "Name" }),
                        _name = new TextBox{ PlaceholderText = "Enter a Name"},
                        new TableCell(null, true)
                    ),
                    new TableRow(
                        new GroupBox {
                            Text = "State values",
                            Content = new TableLayout {
                                Rows =
                                {
                                    new TableRow(
                                        new TableCell(new Label{ Text = "HP: ", }, true),
                                        _hp = new TextBox{ PlaceholderText = "0"}
                                    ),
                                    new TableRow(
                                        new TableCell(new Label{ Text = "Atk: "}),
                                        _atk = new TextBox{ PlaceholderText = "0"}
                                    ),
                                    new TableRow(
                                        new TableCell(new Label{ Text = "Def: "}),
                                        _def = new TextBox{ PlaceholderText = "0"}
                                    ),
                                    new TableRow(
                                        new TableCell(new Label{ Text = "SpAtk: "}, true),
                                        _spAtk = new TextBox{ PlaceholderText = "0"}
                                    ),
                                    new TableRow(
                                        new TableCell(new Label{ Text = "SpDef: "}, true),
                                        _spDef = new TextBox{ PlaceholderText = "0"}
                                    ),
                                    new TableRow(
                                        new TableCell(new Label{ Text = "Speed: "}, true),
                                        _speed = new TextBox{ PlaceholderText = "0"}
                                    ),
                                    new TableRow()
                                }
                            }
                        },
                        new GroupBox {
                            Text = "Types",
                            Content = new TableLayout {
                                Rows = 
                                {
                                    new TableRow(
                                        new TableCell(new Label{ Text = "Type1: "}, true),
                                        new ComboBox{ DataStore = _types, ReadOnly = true, SelectedIndex = 1}
                                    ),
                                    new TableRow(
                                        new TableCell(new Label{ Text = "Type2: "}, true),
                                        new ComboBox{ DataStore = _types, ReadOnly = true, SelectedIndex = 0}
                                    ),
                                    new TableRow()
                                }
                            }  
                        },
                        
                        new TableCell(null, true)
                    ),
                    new TableRow()
                }
            };

            var MainLayout = new Splitter
            {
                Panel1 = FieldsLayout,
                Panel2 = new GroupBox { Content = new ListBox { DataStore = _list } , Text = "Character List" }
            };

            this.Content = MainLayout;
        }
    }
}
