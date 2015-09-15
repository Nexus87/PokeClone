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

namespace ToolsWPF.MoveBuilder
{
    /// <summary>
    /// Interaction logic for MoveBuilder.xaml
    /// </summary>
    public partial class MoveBuilder : UserControl
    {
        public IEnumerable<Base.PokemonType> Types { get; private set; } 
        public Base.PokemonType SelectedType {
            get;
            set;
        }
        public MoveBuilder()
        {
            Types = Globals.TypeList;
            SelectedType = 0;
            InitializeComponent();
        }
    }
}
