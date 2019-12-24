using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public static void InitializeUtilities()
		{
			// Initialize the default units
			ConsumptionUnit = ConsumptionUnit.LITERS_PER_100_KM;
			CurrencyUnit = CurrencyUnit.EURO;

			// Initialize the lists
			VehicleList = new ObservableCollection<Vehicle>() { };
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
