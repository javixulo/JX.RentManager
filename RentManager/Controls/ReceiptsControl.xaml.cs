using System.Windows;
using System.Windows.Input;
using RentManager.Helpers;
using RentManager.Model;
using SQLiteFramework;

namespace RentManager.Controls
{
	public partial class ReceiptsControl
	{
		private Receipt _currentReceipt;

		public ReceiptsControl()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty ContractProperty = DependencyProperty.Register("Contract", typeof(Contract), typeof(ReceiptsControl));

		public Contract Contract
		{
			get { return (Contract)GetValue(ContractProperty); }
			set { SetValue(ContractProperty, value); }
		}

		public static readonly DependencyProperty SelectedReceiptProperty = DependencyProperty.Register("SelectedReceipt", typeof(Receipt), typeof(ReceiptsControl));

		public Receipt SelectedReceipt { get; set; }

		private void NewReceiptClick(object sender, RoutedEventArgs e)
		{
			if (Contract == null)
				return;

			_currentReceipt = new Receipt { Contract = Contract.ID };

			MainWindow.Context.New(_currentReceipt);
		}

		private void OnDeleteReceiptClick(object sender, RoutedEventArgs e)
		{
			if (ReceiptsGrid.SelectedItem == null)
				return;

			if (!MessageHelper.WantToContinue())
				return;

			MainWindow.Context.Delete((SqliteObject)ReceiptsGrid.SelectedItem);
		}

		private void OnReceiptsGridDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (ReceiptsGrid.SelectedItem == null)
				return;

			MainWindow.Context.Edit((SqliteObject)ReceiptsGrid.SelectedItem);
		}
	}
}
