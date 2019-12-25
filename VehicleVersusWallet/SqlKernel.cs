using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace VehicleVersusWallet
{
	public static class SqlKernel
	{
		private static string DatabaseFilename { get; } = "Database.sqlite";

		#region Meta database functions

		/// <summary>
		/// Sets up the database and makes sure it is ready for the application.
		/// </summary>
		public static void InitializeDatabase()
		{
			// Make sure the SQLite database file exists, by creating it if it doesn't already exist.
			if (!File.Exists(DatabaseFilename))
			{
				SQLiteConnection.CreateFile(DatabaseFilename);
				InitializeTables();
			}
		}

		/// <summary>
		/// Create the database tables with their default values.
		/// </summary>
		public static void InitializeTables()
		{
			string vehiclesTableString = "CREATE TABLE VEHICLES (VEHICLE_ID INTEGER PRIMARY KEY UNIQUE NOT NULL, " +
				"NAME VARCHAR NOT NULL, " +
				"VEHICLE_TYPE TEXT NOT NULL, " +
				"PRICE_ORIGINAL DECIMAL NOT NULL, " +
				"CONSUMPTION_CITY_ORIGINAL DECIMAL NOT NULL, " +
				"CONSUMPTION_HIGHWAY_ORIGINAL DECIMAL NOT NULL, " +
				"CONSUMPTION_UNIT_INDEX_ORIGINAL INTEGER NOT NULL, " +
				"CURRENCY_UNIT_INDEX_ORIGINAL INTEGER NOT NULL);";

			string settingsTableString = "CREATE TABLE SETTINGS (WINDOW_WIDTH INTEGER NOT NULL DEFAULT(1200), " +
				"WINDOW_HEIGHT INTEGER NOT NULL	DEFAULT(600), " +
				"FULLSCREEN BOOLEAN NOT NULL DEFAULT(FALSE), " +
				"SELECTED_CONSUMPTION_UNIT_INDEX INTEGER NOT NULL DEFAULT(0), " +
				"SELECTED_CURRENCY_UNIT_INDEX INTEGER NOT NULL DEFAULT(0));";

			string settingsDefaultInsertString = "INSERT INTO SETTINGS DEFAULT VALUES;";

			ExecuteQuery(vehiclesTableString);
			ExecuteQuery(settingsTableString);
			ExecuteQuery(settingsDefaultInsertString);
		}

		/// <summary>
		/// Executes an SQLite query and returns any data provided by the query in the form of a DataTable.
		/// </summary>
		/// <param name="query">The SQLite query to run.</param>
		/// <returns>The data returned by the SQLite query, in the form of a DataTable.</returns>
		public static DataTable ExecuteQuery(string query)
		{
			string connectionString = $"Data Source={DatabaseFilename};Version=3;";
			using (DataSet dataSet = new DataSet())
			{
				using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, connectionString))
				{
					using (TransactionScope transaction = new TransactionScope())
					{
						dataSet.Clear();
						dataAdapter.Fill(dataSet);
						DataTable dataTable = new DataTable();
						if (dataSet.Tables.Count > 0)
							dataTable = dataSet.Tables[0];
						transaction.Complete();
						return dataTable;
					}
				}
			}
		}

		#endregion

		#region Specific database functions

		/// <summary>
		/// Returns all the vehices registered in the database.
		/// </summary>
		/// <returns>A list with Vehicle objects that are found in the database.</returns>
		public static List<Vehicle> GetVehicles()
		{
			// Get the data.
			DataTable dataTable = ExecuteQuery("SELECT * FROM VEHICLES;");

			// Create and populate the vehicles' list.
			List<Vehicle> vehicles = new List<Vehicle>();
			foreach (DataRow dataRow in dataTable.Rows)
			{
				Vehicle vehicle = new Vehicle(dataRow);
				vehicles.Add(vehicle);
			}

			// Return the list, empty or otherwise.
			return vehicles;
		}

		/// <summary>
		/// Adds the given vehicle to the database.
		/// </summary>
		/// <param name="vehicle">THe vehicle object to add to the database.</param>
		public static void AddVehicle(Vehicle vehicle)
		{
			string sqlQuery = $"INSERT INTO VEHICLES (VEHICLE_ID, NAME, VEHICLE_TYPE, PRICE_ORIGINAL, CONSUMPTION_CITY_ORIGINAL, " +
				$"CONSUMPTION_HIGHWAY_ORIGINAL, CONSUMPTION_UNIT_INDEX_ORIGINAL, CURRENCY_UNIT_INDEX_ORIGINAL) VALUES " +
				$"({vehicle.VehicleID}, '{vehicle.NameShort}', {(int)vehicle.VehicleType}, {vehicle.PriceOriginal}, " +
				$"{vehicle.ConsumptionCityOriginal}, {vehicle.ConsumptionHighwayOriginal}, {(int)vehicle.ConsumptionUnitOriginal}, " +
				$"{(int)vehicle.CurrencyUnitOriginal})";

			ExecuteQuery(sqlQuery);
		}

		/// <summary>
		/// Fetches an available ID to be used by a vehicle at its creation.
		/// </summary>
		/// <returns>The available vehicle identifier.</returns>
		public static int GetAvailableVehicleID()
		{
			DataTable dataTable = ExecuteQuery("SELECT MAX(VEHICLE_ID) FROM VEHICLES;");
			string returnString = dataTable.Rows[0]["MAX(VEHICLE_ID)"].ToString();
			return returnString == "" ? 0 : Convert.ToInt32(returnString) + 1;
		}

		/// <summary>
		/// Deletes a vehicle from the database, using its identifier.
		/// </summary>
		/// <param name="vehicleID">The vehicle that we want deleted identifier.</param>
		public static void DeleteVehicle(int vehicleID)
		{
			ExecuteQuery($"DELETE FROM VEHICLES WHERE VEHICLE_ID={vehicleID};");
		}

		#endregion
	}
}
