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
		public AddTransportationWindow()
		{
			InitializeComponent();
		}

		private void AddVehicle_Click(object sender, MouseButtonEventArgs e)
		{

		}

		private void Cancel_Click(object sender, MouseButtonEventArgs e)
		{

		}

		private void CurrencyCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void ConsumptionCityCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void ConsumptionHighwayCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void DistanceCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

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
