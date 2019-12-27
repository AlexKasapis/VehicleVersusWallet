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
		private bool automaticChangingCombobox = false;


		public AddVehicleWindow()
		{
			InitializeComponent();

			// Setup the comboboxes
			VehicleTypeCombobox.ItemsSource = Utilities.VehicleTypesList;
			VehicleTypeCombobox.SelectedIndex = 0;
			CurrencyCombobox.ItemsSource = Utilities.CurrencyUnitList;
			CurrencyCombobox.SelectedIndex = (int)Utilities.CurrencyUnit;
			ConsumptionCityCombobox.ItemsSource = Utilities.ConsumptionUnitList;
			ConsumptionCityCombobox.SelectedIndex = (int)Utilities.ConsumptionUnit;
			ConsumptionHighwayCombobox.ItemsSource = Utilities.ConsumptionUnitList;
			ConsumptionHighwayCombobox.SelectedIndex = (int)Utilities.ConsumptionUnit;
		}

		private void AddVehicle_Click(object sender, MouseButtonEventArgs e)
		{
			Vehicle = new Vehicle()
			{
				VehicleID = SqlKernel.GetAvailableVehicleID(),
				NameShort = NameTextBox.Text,
				PriceOriginal = float.Parse(PriceTextBox.Text, CultureInfo.InvariantCulture.NumberFormat),
				ConsumptionCityOriginal = float.Parse(ConsumptionCityTextBox.Text, CultureInfo.InvariantCulture.NumberFormat),
				ConsumptionHighwayOriginal = float.Parse(ConsumptionHighwayTextBox.Text, CultureInfo.InvariantCulture.NumberFormat),
				ConsumptionUnitOriginal = Utilities.ConsumptionUnit,
				CurrencyUnitOriginal = Utilities.CurrencyUnit,
				VehicleType = (VehicleType)VehicleTypeCombobox.SelectedIndex
			};

			DialogResult = true;
		}

		private void Cancel_Click(object sender, MouseButtonEventArgs e)
		{
			DialogResult = false;
		}

		private void CurrencyCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Utilities.CurrencyUnit = (CurrencyUnit)CurrencyCombobox.SelectedIndex;
		}

		private void ConsumptionCityCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// In order not to start an endless loop, we need a variable that notifies the program when to ignore changing the other combobox
			if (automaticChangingCombobox)
				return;

			Utilities.ConsumptionUnit = (ConsumptionUnit)ConsumptionCityCombobox.SelectedIndex;
			automaticChangingCombobox = true;
			ConsumptionHighwayCombobox.SelectedIndex = ConsumptionCityCombobox.SelectedIndex;
			automaticChangingCombobox = false;
		}

		private void ConsumptionHighwayCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// In order not to start an endless loop, we need a variable that notifies the program when to ignore changing the other combobox
			if (automaticChangingCombobox)
				return;

			Utilities.ConsumptionUnit = (ConsumptionUnit)ConsumptionHighwayCombobox.SelectedIndex;
			automaticChangingCombobox = true;
			ConsumptionCityCombobox.SelectedIndex = ConsumptionHighwayCombobox.SelectedIndex;
			automaticChangingCombobox = false;
		}
	}
}
