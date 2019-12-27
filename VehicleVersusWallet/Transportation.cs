using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleVersusWallet
{
	public class Transportation
	{
		// Identifiers
		public int TransportationID { get; set; }
		public string TransportationName { get; set; }

		// Distances
		public float CityPercentage { get; set; }
		public float HighwayPercentage { get; set; }
		public float TotalDistanceOriginal { get; set; }
		public float TotalDistance { get; }
		public float TotalDistanceCity { get { return (float)Math.Round(TotalDistance * CityPercentage / 100, 2); } }
		public float TotalDistanceHighway { get { return (float)Math.Round(TotalDistance * HighwayPercentage / 100, 2); } }

		// Unit
		public DistanceUnit DistanceUnitOriginal { get; set; }
    }
}
