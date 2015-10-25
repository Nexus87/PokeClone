using Base;
using System.Collections.Generic;
using System.Windows.Controls;

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
