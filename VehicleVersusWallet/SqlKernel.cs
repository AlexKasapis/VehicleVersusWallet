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
				"PRICE DECIMAL NOT NULL, " +
				"CONSUMPTION_CITY DECIMAL NOT NULL, " +
				"CONSUMPTION_HIGHWAY DECIMAL NOT NULL, " +
				"CONSUMPTION_UNIT_ORIGINAL TEXT NOT NULL, " +
				"CURRENCY_UNIT_ORIGINAL TEXT NOT NULL);";

			string settingsTableString = "CREATE TABLE SETTINGS (WINDOW_WIDTH INTEGER NOT NULL DEFAULT(800), " +
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
	}
}
