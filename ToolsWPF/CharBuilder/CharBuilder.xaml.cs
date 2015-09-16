using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToolsWPF
{
    /// <summary>
    /// Interaction logic for CharBuilder.xaml
    /// </summary>
    public partial class CharBuilder : UserControl
    {
        public IEnumerable<PokemonType> Types { get; private set; }
        public List<PKData> Data { get; set; }
        
        public CharBuilder()
        {
            Types = Globals.TypeList;
            Data = new List<PKData>();
            Data.Add(new PKData { Name = "Data1", BaseStats = new Stats()});

            InitializeComponent();
        }
    }
}
