using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleVersusWallet
{
	public class Vehicle
	{
		// Vehicle identification. Used to identify a vehicle in the database.
		public int VehicleID { get; set; }

		// Vehicle given name. The long name comes with the information about the vehicle type.
		public string NameShort { get; set; }
		public string NameLong { get { return $"{NameShort} ({Utilities.VehicleTypesList[(int)VehicleType]})"; } }

		// The type of the vehicle
		public VehicleType VehicleType { get; set; }

		// Starting prices. The original price is the one given, while the other two go through a unit transformation.
		public float PriceOriginal { get; set; }
		public float PriceShort { get { return Utilities.GetLocalPriceValue(PriceOriginal, CurrencyUnitOriginal); } }
		public string PriceLong { get { return $"{PriceShort}{Utilities.GetCurrencyUnit()}"; } }

		// Fuel consumption values. The originals are the one given, while the others go through a unit transformation.
		public float ConsumptionCityOriginal { get; set; }
		public float ConsumptionCityShort { get { return Utilities.GetLocalConsumptionValue(ConsumptionCityOriginal, ConsumptionUnitOriginal); } }
		public string ConsumptionCityLong { get { return $"{ConsumptionCityShort}{Utilities.GetConsumptionUnit()}"; } }
		public float ConsumptionHighwayOriginal { get; set; }
		public float ConsumptionHighwayShort { get { return Utilities.GetLocalConsumptionValue(ConsumptionHighwayOriginal, ConsumptionUnitOriginal); } }
		public string ConsumptionHighwayLong { get { return $"{ConsumptionHighwayShort}{Utilities.GetConsumptionUnit()}"; } }
		public string ConsumptionTotal { get { return $@"{ConsumptionCityShort}/{ConsumptionHighwayShort}({Utilities.GetConsumptionUnit()})"; } }

		// The original units used when the price and consumption of the vehicle were registered. Used for unit convertion.
		public ConsumptionUnit ConsumptionUnitOriginal { get; set; }
		public CurrencyUnit CurrencyUnitOriginal { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Vehicle() { }

		/// <summary>
		/// Creates a Vehicle object using a DataRow. Used when retrieving vehicles from the database.
		/// </summary>
		/// <param name="dataRow">A DataRow object with data fetched from the database.</param>
		public Vehicle(DataRow dataRow)
		{
			VehicleID = Convert.ToInt32(dataRow["VEHICLE_ID"]);
			NameShort = dataRow["NAME"].ToString();
			PriceOriginal = float.Parse(dataRow["PRICE_ORIGINAL"].ToString());
			ConsumptionCityOriginal = float.Parse(dataRow["CONSUMPTION_CITY_ORIGINAL"].ToString());
			ConsumptionHighwayOriginal = float.Parse(dataRow["CONSUMPTION_HIGHWAY_ORIGINAL"].ToString());
			ConsumptionUnitOriginal = (ConsumptionUnit)Convert.ToInt32(dataRow["CONSUMPTION_UNIT_INDEX_ORIGINAL"]);
			CurrencyUnitOriginal = (CurrencyUnit)Convert.ToInt32(dataRow["CURRENCY_UNIT_INDEX_ORIGINAL"]);
		}
	}
}
