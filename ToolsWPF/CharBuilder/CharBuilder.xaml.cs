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
        public PKData Data { get; set; }
        
        public CharBuilder()
        {
            Types = Globals.TypeList;
            Data = new PKData();
            Data.Type1 = PokemonType.Normal;
            Data.Type2 = PokemonType.None;

            InitializeComponent();
        }
    }
}
