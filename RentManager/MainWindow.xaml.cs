using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RentManager.Controls;
using RentManager.Helpers;
using RentManager.Model;
using RentManager.Windows;

namespace RentManager
{
	public partial class MainWindow
	{
		public static  RentManagerDataContext Context;
		public MainWindow()
		{
			InitializeComponent();

			Context = new RentManagerDataContext();

			DataContext = Context;

			Closed += OnClosed;
		}

		private void OnClosed(object sender, EventArgs eventArgs)
		{
			Context.Close();
		}

		#region PersonBases

		private void OnDataGridDoubleClick(object sender, PersonDataGrid.SqliteObjectEventArgs args)
		{
			Context.Edit(args.SqliteObject);
		}

		private void OnDelete(object sender, PersonDataGrid.SqliteObjectEventArgs args)
		{
			Context.Delete(args.SqliteObject);
		}

		private void NewOwnerClick(object sender, RoutedEventArgs e)
		{
			Context.New(new Owner());
		}

		private void NewTenantClick(object sender, RoutedEventArgs e)
		{
			Context.New(new Tenant());
		}

		private void NewGuarantorClick(object sender, RoutedEventArgs e)
		{
			Context.New(new Guarantor());
		}

		#endregion

		#region Properties


		#endregion
	}
}
