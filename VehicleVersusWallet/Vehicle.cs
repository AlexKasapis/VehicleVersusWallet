using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleVersusWallet
{
	public class Vehicle
	{
		private float priceOriginal;
		private float consumptionCityOriginal;
		private float consumptionHighwayOriginal;
		private ConsumptionUnit consumptionUnitOriginal;
		private CurrencyUnit currencyUnitOriginal;
		private VehicleType vehicleType;

		// These units use the original saved values to convert to the local format
		public string NameShort { get; private set; }
		public string NameLong { get { return $"{NameShort} ({Utilities.VehicleTypesList[(int)vehicleType]})"; } }

		public float PriceShort { get { return Utilities.GetLocalPriceValue(priceOriginal, currencyUnitOriginal); } }
		public string PriceLong { get { return $"{PriceShort}{Utilities.GetCurrencyUnit()}"; } }

		public float ConsumptionCityShort { get { return Utilities.GetLocalConsumptionValue(consumptionCityOriginal, consumptionUnitOriginal); } }
		public string ConsumptionCityLong { get { return $"{ConsumptionCityShort}{Utilities.GetConsumptionUnit()}"; } }

		public float ConsumptionHighwayShort { get { return Utilities.GetLocalConsumptionValue(consumptionHighwayOriginal, consumptionUnitOriginal); } }
		public string ConsumptionHighwayLong { get { return $"{ConsumptionHighwayShort}{Utilities.GetConsumptionUnit()}"; } }

		public string ConsumptionTotal { get { return $@"{ConsumptionCityShort}/{ConsumptionHighwayShort}({Utilities.GetConsumptionUnit()})"; } }

		public Vehicle(string _name, float _price, float _consCity, float _consHigh, ConsumptionUnit _consUnit, CurrencyUnit _currUnit, VehicleType _vehType)
		{
			NameShort = _name;
			priceOriginal = _price;
			consumptionCityOriginal = _consCity;
			consumptionHighwayOriginal = _consHigh;
			consumptionUnitOriginal = _consUnit;
			currencyUnitOriginal = _currUnit;
			vehicleType = _vehType;
		}
		
		public void ModifyVehicle(string _name, float _price, float _consCity, float _consHigh, ConsumptionUnit _consUnit, CurrencyUnit _currUnit, VehicleType _vehType)
		{
			NameShort = _name;
			priceOriginal = _price;
			consumptionCityOriginal = _consCity;
			consumptionHighwayOriginal = _consHigh;
			consumptionUnitOriginal = _consUnit;
			currencyUnitOriginal = _currUnit;
			vehicleType = _vehType;
		}
	}
}
