using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleVersusWallet
{
	public enum ConsumptionUnits
	{
		LITERS_PER_100_KM, GALLONS_PER_100_MILES_US, GALLONS_PER_100_MILES_UK,
		KM_PER_LITER, MILES_PER_GALLON_US, MILES_PER_GALLON_UK
	}

	public enum CurrencyUnits
	{
		EURO, US_DOLLAR, POUND
	}

	public class Vehicle
	{
		public string Name { get; set; }
		public float Price { get; set; }
		public float ConsumptionCity { get; set; }
		public float ConsumptionHighway { get; set; }
		public string Consumption { get { return $@"{ConsumptionCity}(City)/{Consumption}"; } }

		public Vehicle() { }
	}
}
