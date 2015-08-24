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
		TextBox _id;
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

		TableRow BaseRow() {
			return new TableRow (
				new TableLayout{
					Spacing = new Size(5, 5),
					Rows = 
					{
						new TableRow(
							new Label { Text = "Id:"},
							_id = new TextBox { PlaceholderText = "Please enter an id"}
						),
						new TableRow(
							new TableCell (new Label{ Text = "Name:" }),
							_name = new TextBox{ PlaceholderText = "Please enter a name" }
						)
					}
				}
			);
		}
		TableRow StatsRow(){
			return new TableRow(
				new GroupBox {
					Text = "State values",
					Content = new TableLayout {
						Spacing = new Size(5, 5),
						Padding = new Padding(5, 5),
						Rows =
						{
							new TableRow(
								new TableCell(new Label{ Text = "HP: ", }),
								_hp = new TextBox{ PlaceholderText = "0"},
								new TableCell(new Label{ Text = "Atk: "}),
								_atk = new TextBox{ PlaceholderText = "0"},
								new TableCell(new Label{ Text = "Def: "}),
								_def = new TextBox{ PlaceholderText = "0"},
								new TableCell(null, true)
							),

							new TableRow(
								new TableCell(new Label{ Text = "Speed: "}),
								_speed = new TextBox{ PlaceholderText = "0"},
								new TableCell(new Label{ Text = "SpAtk: "}),
								_spAtk = new TextBox{ PlaceholderText = "0"},
								new TableCell(new Label{ Text = "SpDef: "}),
								_spDef = new TextBox{ PlaceholderText = "0"},
								new TableCell(null, true)
							),
							new TableRow()
						}
					}
				}
			);
		}

		TableRow TypeRow() {
			return new TableRow (
				new GroupBox {
					Text = "Types",
					Content = new TableLayout {
						Spacing = new Size (5, 5),
						Padding = new Padding (5, 5),
						Rows = {
							new TableRow (
								new TableCell (new Label{ Text = "Type1: " }),
								new ComboBox{ DataStore = _types, ReadOnly = true, SelectedIndex = (int) PokemonType.Normal },
								new TableCell (new Label{ Text = "Type2: " }),
								new ComboBox{ DataStore = _types, ReadOnly = true, SelectedIndex = (int) PokemonType.None }
							),
							new TableRow ()
						}
					}  
				},
				new TableCell(null, true)
			);
		}

        void init()
        {
            var FieldsLayout = new TableLayout
            {
                Padding = new Padding(5),
                Spacing = new Size(5, 5),
                Rows =
                {
					BaseRow(),   
					StatsRow(),
					TypeRow(),
                    new TableRow()
                }
            };

            var MainLayout = new TableLayout
            (
                new TableRow(
					new TableCell(FieldsLayout, true),
                	new GroupBox { Content = new ListBox { DataStore = _list } , Text = "Character List" }
				)
			);

            this.Content = MainLayout;
        }
    }
}
