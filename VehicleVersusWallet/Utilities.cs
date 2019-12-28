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

	public enum DistanceUnit
	{
		KILOMETERS, MILES
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
		public static List<string> ConsumptionUnitList = new List<string>
		{
			"Lt/100km", "G/100Mi(US)", "G/100Mi(UK)", "km/Lt", "Mi/G(US)", "Mi/G(UK)"
		};
		public static List<string> CurrencyUnitList = new List<string> { "Euro (€)", "US Dollar ($)", "Pound (£)" };
		public static List<string> CurrencyUnitSymbolList = new List<string> { "€", "$", "£" };
		public static List<string> VehicleTypesList = new List<string> { "Car", "Motorcycle" };

		// Currently selected units
		public static ConsumptionUnit ConsumptionUnit { get; set; }
		public static CurrencyUnit CurrencyUnit { get; set; }
		public static DistanceUnit DistanceUnit { get; set; }

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
			ConsumptionUnit = (ConsumptionUnit)Convert.ToInt32(settingsDataRow["SELECTED_CONSUMPTION_UNIT_INDEX"]);
			CurrencyUnit = (CurrencyUnit)Convert.ToInt32(settingsDataRow["SELECTED_CURRENCY_UNIT_INDEX"]);
			DistanceUnit = (DistanceUnit)Convert.ToInt32(settingsDataRow["SELECTED_DISTANCE_UNIT_INDEX"]);

			// Initialize the lists
			VehicleList = new ObservableCollection<Vehicle>(SqlKernel.GetVehicles());
		}

		public static string GetCurrencyUnit(bool symbolOnly)
		{
			return symbolOnly ? CurrencyUnitSymbolList[(int)CurrencyUnit] : CurrencyUnitList[(int)CurrencyUnit];
		}

		public static string GetConsumptionUnit()
		{
			return ConsumptionUnitList[(int)ConsumptionUnit];
		}

		public static float GetLocalPriceValue(float originalValue, CurrencyUnit originalUnit)
		{
			// These exchange rates are hardcoded and temporary until I write an automatic fetcher of exchange rates.
			const float EUR_TO_USD_RATE = 1.11f;
			const float EUR_TO_POUND_RATE = 0.85f;
			const float USD_TO_POUND_RATE = 0.77f;

			float convertedValue = originalValue;

			if (originalUnit == CurrencyUnit.EURO)
			{
				if (CurrencyUnit == CurrencyUnit.US_DOLLAR)
					convertedValue *= EUR_TO_USD_RATE;
				else if (CurrencyUnit == CurrencyUnit.POUND)
					convertedValue *= EUR_TO_POUND_RATE;
			}
			else if (originalUnit == CurrencyUnit.US_DOLLAR)
			{
				if (CurrencyUnit == CurrencyUnit.EURO)
					convertedValue *= 1 / EUR_TO_USD_RATE;
				else if (CurrencyUnit == CurrencyUnit.POUND)
					convertedValue *= USD_TO_POUND_RATE;
			}
			else if (originalUnit == CurrencyUnit.POUND)
			{
				if (CurrencyUnit == CurrencyUnit.EURO)
					convertedValue *= 1 / EUR_TO_POUND_RATE;
				else if (CurrencyUnit == CurrencyUnit.US_DOLLAR)
					convertedValue *= 1 / USD_TO_POUND_RATE;
			}

			return (float)Math.Round(convertedValue, 2);
		}

		public static float GetLocalConsumptionValue(float originalValue, ConsumptionUnit originalUnit)
		{
			// Unit convertion multipliers. For the opposite convertion multiply with (1/multiplier).
			const float LITRE_TO_GALLON_UK_MULTIPLIER = 0.219969f;
			const float LITRE_TO_GALLON_US_MULTIPLIER = 0.264171f;
			const float GALLON_UK_TO_GALLON_US_MULTIPLIER = 1.20095f;
			const float KILOMETRE_TO_MILE_MULTIPLIER = 0.621371f;

			// The convertion comes in two steps. First, if we need to swap the form of the format
			// (ie FuelVolume/Distance <-> Distance/FuelVolume) do that. Then, we make any unit transformations
			// (ie between US, Imperial and Metric).
			float convertedValue = originalValue;
			if ((int)originalUnit % 3 - (int)ConsumptionUnit % 3 != 0)  // The one needs to be 0,1,2 and the other 3,4,5
				convertedValue = 100 / convertedValue;

			string[] unitCodes = new string[] { "METRIC", "US", "UK", "METRIC", "US", "UK" };
			string unitStart = unitCodes[(int)originalUnit];
			string unitFinal = unitCodes[(int)ConsumptionUnit];

			if (unitStart == "METRIC")
			{
				if (unitFinal == "US")
					convertedValue *= LITRE_TO_GALLON_US_MULTIPLIER / KILOMETRE_TO_MILE_MULTIPLIER;
				else if (unitFinal == "UK")
					convertedValue *= LITRE_TO_GALLON_UK_MULTIPLIER / KILOMETRE_TO_MILE_MULTIPLIER;
			}
			else if (unitStart == "US")
			{
				if (unitFinal == "METRIC")
					convertedValue *= KILOMETRE_TO_MILE_MULTIPLIER / LITRE_TO_GALLON_US_MULTIPLIER;
				else if (unitFinal == "UK")
					convertedValue *= 1 / GALLON_UK_TO_GALLON_US_MULTIPLIER;
			}
			else if (unitStart == "UK")
			{
				if (unitFinal == "METRIC")
					convertedValue *= KILOMETRE_TO_MILE_MULTIPLIER / LITRE_TO_GALLON_UK_MULTIPLIER;
				else if (unitFinal == "US")
					convertedValue *= GALLON_UK_TO_GALLON_US_MULTIPLIER;
			}

			return (float)Math.Round(convertedValue, 2);
		}

		public static float GetLocalDistanceValue(float originalValue, DistanceUnit originalUnit)
		{
			// Convertion multipliers
			const float KILOMETER_TO_MILE_MULTIPLIER = 0.621371f;

			// Make the convertion
			float convertedValue = originalValue;
			if (originalUnit == DistanceUnit.KILOMETERS && DistanceUnit == DistanceUnit.MILES)
				convertedValue *= KILOMETER_TO_MILE_MULTIPLIER;
			else if (originalUnit == DistanceUnit.MILES && DistanceUnit == DistanceUnit.KILOMETERS)
				convertedValue *= 1 / KILOMETER_TO_MILE_MULTIPLIER;

			// Return the converted value
			return convertedValue;
		}
	}
}
