using Base;
using Base.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ToolsWPF.MoveBuilder
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class NegateConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value;
        }

    }
    [ValueConversion(typeof(int?), typeof(bool))]
    public class NullIntConvert : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? (int?) null : 0;
        }
    }

    public class NumberValidator : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "Value ca not be empty");

            return Regex.IsMatch(value.ToString(), @"\d+") ?
                ValidationResult.ValidResult : new ValidationResult(false, "Only numbers allowed");


        }
    }
    /// <summary>
    /// Interaction logic for MoveBuilder.xaml
    /// </summary>
    public partial class MoveBuilder : UserControl
    {
        public ObservableCollection<MoveData> Data { get; set; }
        public IEnumerable<PokemonType> Types { get; set; }
        public MoveBuilder()
        {
            Data = new ObservableCollection<MoveData>();
            newItem();
            Types = Globals.TypeList;
            InitializeComponent();
        }

        private MoveData newItem()
        {
            string newMove = "NewMove";
            var move =new MoveData { Name = newMove };
            Data.Add(move);
            return move;
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            moveList.SelectedIndex = Data.IndexOf(newItem());
        }
    }
}
