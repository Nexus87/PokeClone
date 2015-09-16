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
using Base;

namespace ToolsWPF.MoveBuilder
{
    /// <summary>
    /// Interaction logic for MoveBuilder.xaml
    /// </summary>
    public partial class MoveBuilder : UserControl
    {
        public List<MoveData> Data { get; set; }
        public IEnumerable<PokemonType> Types { get; set; }
        public MoveBuilder()
        {
            Data = new List<MoveData>();
            Data.Add(new MoveData { Name = "Name1" });
            Data.Add(new MoveData { Name = "Name2" });
            Types = Globals.TypeList;
            InitializeComponent();
        }
    }
}
