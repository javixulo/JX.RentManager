using System.Windows;
using System.Windows.Input;
using RentManager.Helpers;
using RentManager.Model;
using SQLiteFramework;

namespace RentManager.Controls
{
	public partial class PaymentsControl 
	{
		public PaymentsControl()
		{
			InitializeComponent();
		}

		private Payment _currentPayment;

		public static readonly DependencyProperty ReceiptProperty = DependencyProperty.Register("Receipt", typeof(Receipt), typeof(PaymentsControl));

		public Receipt Receipt
		{
			get { return (Receipt)GetValue(ReceiptProperty); }
			set { SetValue(ReceiptProperty, value); }
		}

		private void NewPaymentClick(object sender, RoutedEventArgs e)
		{
			if (Receipt == null)
				return;

			_currentPayment = new Payment { Receipt = Receipt.ID };

			MainWindow.Context.New(_currentPayment);
		}

		private void OnDeletePaymentClick(object sender, RoutedEventArgs e)
		{
			if (PaymentsGrid.SelectedItem == null)
				return;

			if (!MessageHelper.WantToContinue())
				return;

			MainWindow.Context.Delete((SqliteObject)PaymentsGrid.SelectedItem);
		}

		private void OnPaymentGridDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (PaymentsGrid.SelectedItem == null)
				return;

			MainWindow.Context.Edit((SqliteObject)PaymentsGrid.SelectedItem);
		}
	}
}
