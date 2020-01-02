using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace VehicleVersusWallet
{
	/// <summary>
	/// Interaction logic for AddTransportationWindow.xaml
	/// </summary>
	public partial class AddTransportationWindow : Window
	{
		public Transportation Transportation;

		public AddTransportationWindow()
		{
			InitializeComponent();

			// Setup the combobox
			DistanceTypeComboBox.ItemsSource = Utilities.DistanceUnitList;
			DistanceTypeComboBox.SelectedIndex = (int)Utilities.DistanceUnit;
		}

		private void AddTransportation_Click(object sender, MouseButtonEventArgs e)
		{
			Transportation = new Transportation()
			{
				TransportationID = SqlKernel.GetAvailableTransportationID(),
				TransportationName = NameTextBox.Text,
				CityPercentage = 100 - (int)RoadTypeSlider.Value,
				HighwayPercentage = (int)RoadTypeSlider.Value,
				TotalDistanceOriginal = float.Parse(DistanceTextBox.Text),
				DistanceUnitOriginal = (DistanceUnit)DistanceTypeComboBox.SelectedIndex,
				RepeatFrequency = RepeatFrequency.WEEK,
				RepeatAmount = 5
			};

			DialogResult = true;
		}

		private void Cancel_Click(object sender, MouseButtonEventArgs e)
		{
			DialogResult = false;
		}

		private void DistanceTypeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Utilities.DistanceUnit = (DistanceUnit)DistanceTypeComboBox.SelectedIndex;
		}
	}

	public class RoadTypePercentageConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			int sliderValue = System.Convert.ToInt32(values[0]);
			bool cityPercentage = values[1] as string == "CityPercentageTextBlock";
			return $"{(cityPercentage ? "City" : "Highway")} {(cityPercentage ? 100 - sliderValue : sliderValue)}%";
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
