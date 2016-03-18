using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RentManager.Helpers;
using RentManager.Model;
using SQLiteFramework;

namespace RentManager.Controls
{
	public partial class PropertiesControl
	{
		public static readonly DependencyProperty ItemsSourceProperty = ItemsControl.ItemsSourceProperty.AddOwner(typeof(PropertiesControl));

		public IEnumerable<Property> ItemsSource
		{
			get { return (IEnumerable<Property>)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public PropertiesControl()
		{
			InitializeComponent();
		}

		private void NewPropertyClick(object sender, RoutedEventArgs e)
		{
			MainWindow.Context.New(new Property());
		}

		private void OnDeletePropertyClick(object sender, RoutedEventArgs e)
		{
			if (PropertiesGrid.SelectedItem == null)
				return;

			if (!MessageHelper.WantToContinue())
				return;

			MainWindow.Context.Delete((SqliteObject)PropertiesGrid.SelectedItem);
		}

		private void OnPropertiesGridDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (PropertiesGrid.SelectedItem == null)
				return;

			MainWindow.Context.Edit((SqliteObject)PropertiesGrid.SelectedItem);
		}


	}
}
