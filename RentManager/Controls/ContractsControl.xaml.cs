using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RentManager.Helpers;
using RentManager.Model;
using RentManager.Windows;
using SQLiteFramework;

namespace RentManager.Controls
{
	
	public partial class ContractsControl
	{
		public ContractsControl()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty ItemsSourceProperty = ItemsControl.ItemsSourceProperty.AddOwner(typeof(ContractsControl));

		public IEnumerable<Contract> ItemsSource
		{
			get { return (IEnumerable<Contract>)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public static readonly DependencyProperty SelectedContractProperty = DependencyProperty.Register("SelectedContract", typeof(Contract), typeof(ContractsControl));

		public Contract SelectedContract { get; set; }

		private void NewContractClick(object sender, RoutedEventArgs e)
		{
			AddContractWindow window = new AddContractWindow();
			window.ShowDialog();
		}

		private void OnDeleteContractClick(object sender, RoutedEventArgs e)
		{
			if (DeleteConfirmed == null || ContractsGrid.SelectedItem == null)
				return;

			if (!MessageHelper.WantToContinue())
				return;

			DeleteConfirmed(sender, new PersonDataGrid.SqliteObjectEventArgs((SqliteObject)ContractsGrid.SelectedItem));
		}

		private void OnContractsGridDoubleClick(object sender, MouseButtonEventArgs e)
		{
			AddContractWindow window = new AddContractWindow((Contract)ContractsGrid.SelectedItem);
			window.ShowDialog();
		}

		public event PersonDataGrid.SqliteObjectSelectedEventHandler DeleteConfirmed;
	}
}
