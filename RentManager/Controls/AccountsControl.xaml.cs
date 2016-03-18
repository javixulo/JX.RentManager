using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using JXWPFToolkit.Controls;
using JXWPFToolkit.Windows;
using RentManager.Helpers;
using RentManager.Model;

namespace RentManager.Controls
{
	public partial class AccountsControl
	{
		public AccountsControl()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty AccountOwnerProperty = DependencyProperty.Register("AccountOwner", typeof(IAccountOwner), typeof(AccountsControl));

		public IAccountOwner AccountOwner
		{
			get { return (IAccountOwner)GetValue(AccountOwnerProperty); }
			set { SetValue(AccountOwnerProperty, value); }
		}

		private void NewAccountClick(object sender, RoutedEventArgs e)
		{
			List<InputValuesControl.InputItem> items = new List<InputValuesControl.InputItem> { new InputValuesControl.InputItem("Numero", typeof(string)) { Label = "CCC: ", Required = true } };
			InputValuesWindow window = new InputValuesWindow(items, WindowSize.Small, Orientation.Horizontal, 1);
			window.InputFinished += OnInputAccountFinished;
			window.Show();
		}

		private void OnDeleteAccountClick(object sender, RoutedEventArgs e)
		{
			if (AccountsGrid.SelectedItem == null)
				return;

			if (!MessageHelper.WantToContinue())
				return;

			string account = (string)AccountsGrid.SelectedItem;

			AccountOwner.RemoveAccount(account);

			//AccountsGrid.ItemsSource = null;
			//AccountsGrid.ItemsSource = AccountOwner.Accounts;

		}

		private void OnInputAccountFinished(object sender, InputValuesWindow.InputFinishedEventArgs e)
		{
			string account = (string)e.Items.First().Value;
			AccountOwner.AddAccount(account);

			//AccountsGrid.ItemsSource = null;
			//AccountsGrid.ItemsSource = AccountOwner.Accounts;

			Window window = sender as Window;
			if (window != null)
				window.Close();
		}

	}
}
