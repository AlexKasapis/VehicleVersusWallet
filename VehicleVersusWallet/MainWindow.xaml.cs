using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VehicleVersusWallet
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			SqlKernel.InitializeDatabase();
			Utilities.InitializeUtilities();

			VehiclesList.ItemsSource = Utilities.VehicleList;
		}

		private void AddVehicle_Click(object sender, MouseButtonEventArgs e)
		{
			AddVehicleWindow addVehicleWindow = new AddVehicleWindow();
			addVehicleWindow.ShowDialog();
			if (addVehicleWindow.DialogResult == false)
				return;

			Utilities.VehicleList.Add(addVehicleWindow.Vehicle);
		}

		private void AddTransportation_Click(object sender, MouseButtonEventArgs e)
		{

		}
	}
}
