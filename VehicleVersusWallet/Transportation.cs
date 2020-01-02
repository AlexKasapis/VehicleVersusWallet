using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleVersusWallet
{
	public class Transportation
	{
		// Identifiers
		public int TransportationID { get; set; }  // The unique identifier of the transportation, used mainly for database functions.
		public string TransportationName { get; set; }  // Transportation given name.

		// Distances
		public int CityPercentage { get; set; }  // How much of the transportation is on city roads.
		public int HighwayPercentage { get; set; }  // How much of the transportation is on highway roads.
		public float TotalDistanceOriginal { get; set; }  // The total distance of the transportation as registered on the original unit.
		public float TotalDistance { get { return Utilities.GetLocalDistanceValue(TotalDistanceOriginal, DistanceUnitOriginal); } }  // Converted distance to the currently selected distance unit.
		public float TotalDistanceCity { get { return (float)Math.Round(TotalDistance * CityPercentage / 100, 2); } }  // Total amount of distance on city roads after conversion.
		public float TotalDistanceHighway { get { return (float)Math.Round(TotalDistance * HighwayPercentage / 100, 2); } }  // Total amount of distance on highway roads after conversion.
		public string TotalDistanceShortString { get { return $"{TotalDistance}{Utilities.GetDistanceUnit()}"; } }  // Used to display user friendly information
		public string TotalDistanceLongString { get { return $@"{TotalDistanceCity}%/{TotalDistanceHighway}%"; } }  // Used to display user friendly information.
		public string DistancePercentageLongString { get { return $@"{CityPercentage}%/{HighwayPercentage}%"; } }  // Used to display user friendly information.

		// Unit
		public DistanceUnit DistanceUnitOriginal { get; set; }  // The originally selected distance unit at registration. Will be used when converting units.

		// Repetition
		public RepeatFrequency RepeatFrequency { get; set; }  // The base that defines the transportation schedules frequency.
		public int RepeatAmount { get; set; }  // How many times per frequency this transportation takes place.
		public string RepetitionString { get { return $"{RepeatAmount} times per {Utilities.RepeatFrequencyList[(int)RepeatFrequency]}"; } }  // Used to display a comprehensive view on the repetition of the transportation registered.

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Transportation() { }

		/// <summary>
		/// Creates a Transportation object using a DataRow. Used when retrieving transportations from the database.
		/// </summary>
		/// <param name="dataRow">A DataRow object with data fetched from the database.</param>
		public Transportation(DataRow dataRow)
		{
			TransportationID = Convert.ToInt32(dataRow["TRANSPORTATION_ID"]);
			TransportationName = dataRow["NAME"].ToString();
			CityPercentage = Convert.ToInt32(dataRow["CITY_PERCENTAGE"]);
			HighwayPercentage = Convert.ToInt32(dataRow["HIGHWAY_PERCENTAGE"]);
			TotalDistanceOriginal = float.Parse(dataRow["DISTANCE_ORIGINAL"].ToString());
			DistanceUnitOriginal = (DistanceUnit)Convert.ToInt32(dataRow["SELECTED_DISTANCE_UNIT_INDEX"]);
		}
	}
}
