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
	public partial class AddVehicleWindow : Window
	{
		public Vehicle Vehicle;

		public AddVehicleWindow()
		{
			InitializeComponent();
		}

		private void AddVehicle_Click(object sender, RoutedEventArgs e)
		{
			Vehicle = new Vehicle()
			{
				Name = NameTextBox.Text,
				Price = float.Parse(PriceTextBox.Text, CultureInfo.InvariantCulture.NumberFormat),
				ConsumptionCity = float.Parse(ConsumptionCityTextBox.Text, CultureInfo.InvariantCulture.NumberFormat),
				ConsumptionHighway = float.Parse(ConsumptionHighwayTextBox.Text, CultureInfo.InvariantCulture.NumberFormat)
			};
			DialogResult = true;
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
	}
}
