using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VehicleVersusWallet
{
	public enum ConsumptionUnit
	{
		LITERS_PER_100_KM, GALLONS_PER_100_MILES_US, GALLONS_PER_100_MILES_UK,
		KM_PER_LITER, MILES_PER_GALLON_US, MILES_PER_GALLON_UK
	}

	public enum CurrencyUnit
	{
		EURO, US_DOLLAR, POUND
	}

	public enum VehicleType
	{
		CAR, MOTORCYCLE
	}

	public static class Utilities
	{
		// The indexes of the values below must correspond to the value of each unit in the enumerations
		public static List<string> ConsumptionUnitList = new List<string> { "Lt/100km", "G/100Mi(US)", "G/100Mi(UK)", "km/Lt", "Mi/G(US)", "Mi/G(UK)" };
		public static List<string> CurrencyUnitList = new List<string> { "Euro (€)", "US Dollar ($)", "Pound (£)" };
		public static List<string> VehicleTypesList = new List<string> { "Car", "Motorcycle" };

		// Currently selected units
		public static ConsumptionUnit ConsumptionUnit { get; set; }
		public static CurrencyUnit CurrencyUnit { get; set; }

		public static ObservableCollection<Vehicle> VehicleList;

		/// <summary>
		/// Loads any precursor data needed for the application. These data are the application settings and any
		/// saved data the user created.
		/// </summary>
		/// <param name="mainWindow"></param>
		public static void InitializeUtilities(MainWindow mainWindow)
		{
			// Retreive the settings DataTable from the database
			DataTable settingsDataTable = SqlKernel.ExecuteQuery("SELECT * FROM SETTINGS;");
			DataRow settingsDataRow = settingsDataTable.Rows[0];

			// Initialize the window properties
			mainWindow.Width = Convert.ToInt32(settingsDataRow["WINDOW_WIDTH"]);
			mainWindow.Height = Convert.ToInt32(settingsDataRow["WINDOW_HEIGHT"]);
			mainWindow.WindowState = Convert.ToInt32(settingsDataRow["FULLSCREEN"]) == 1 ? WindowState.Maximized : WindowState.Normal;

			// Initialize the default units
			ConsumptionUnit = (ConsumptionUnit)Convert.ToInt32(settingsDataTable.Rows[0]["SELECTED_CONSUMPTION_UNIT_INDEX"]);
			CurrencyUnit = (CurrencyUnit)Convert.ToInt32(settingsDataTable.Rows[0]["SELECTED_CURRENCY_UNIT_INDEX"]);

			// Initialize the lists
			VehicleList = new ObservableCollection<Vehicle>(SqlKernel.GetVehicles());
		}

		public static string GetCurrencyUnit() { return CurrencyUnitList[(int)CurrencyUnit]; }

		public static string GetConsumptionUnit() { return ConsumptionUnitList[(int)ConsumptionUnit]; }

		public static float GetLocalPriceValue(float originalPrice, CurrencyUnit originalUnit)
		{
			return 1;
		}

		public static float GetLocalConsumptionValue(float originalConsumption, ConsumptionUnit originalUnit)
		{
			return 1;
		}
	}
}
