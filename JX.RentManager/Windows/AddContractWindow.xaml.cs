using System.Windows;
using JX.RentManager.Model;

namespace JX.RentManager.Windows
{
	public partial class AddContractWindow
	{
		private readonly Contract _contract;
		private readonly bool _isNew;

		public AddContractWindow(Contract contract = null)
		{
			InitializeComponent();

			if (contract == null)
			{
				_isNew = true;
				_contract = new Contract();
			}
			else
				_contract = contract;
			
			DataContext = _contract;
		}

		private void OnButtonAddPropertyClick(object sender, RoutedEventArgs e)
		{
			SelectObjectWindow window = new SelectObjectWindow(MainWindow.Context.Properties);
			window.ShowDialog();

			if (!(window.SelectedObject is Property))
				return;
			
			Property property = (Property) window.SelectedObject;
			_contract.Property = property.ID;

			DataContext = null;
			DataContext = _contract;

		}

		private void OnButtonAddTenantClick(object sender, RoutedEventArgs e)
		{
			SelectObjectWindow window = new SelectObjectWindow(MainWindow.Context.Tenants);
			window.ShowDialog();

			if (!(window.SelectedObject is Tenant))
				return;

			Tenant tenant = (Tenant)window.SelectedObject;
			_contract.Tenant = tenant.DNI;

			DataContext = null;
			DataContext = _contract;

		}

		private void OnButtonAddGuarantorClick(object sender, RoutedEventArgs e)
		{
			SelectObjectWindow window = new SelectObjectWindow(MainWindow.Context.Guarantors);
			window.ShowDialog();

			if (!(window.SelectedObject is Guarantor))
				return;

			Guarantor guarantor = (Guarantor)window.SelectedObject;
			_contract.Guarantor = guarantor.DNI;

			DataContext = null;
			DataContext = _contract;

		}

		private void OnButtonSaveClick(object sender, RoutedEventArgs e)
		{
			int rowsAffected = _contract.Save(RentManagerDataContext.DBConnection, _isNew);

			if ( rowsAffected == 1)
			{
				MainWindow.Context.Contracts.Add(_contract);
				Close();
			}
		}
	}
}
