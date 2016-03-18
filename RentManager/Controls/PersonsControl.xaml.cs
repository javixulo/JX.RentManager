using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RentManager.Helpers;
using RentManager.Model;
using RentManager.Windows;
using SQLiteFramework;

namespace RentManager.Controls
{
	public partial class PersonDataGrid
	{
		public PersonDataGrid()
		{
			InitializeComponent();
		}

		#region properties

		public static readonly DependencyProperty ItemsSourceProperty = ItemsControl.ItemsSourceProperty.AddOwner(typeof(PersonDataGrid));

		public IEnumerable<PersonBase> ItemsSource
		{
			get { return (IEnumerable<PersonBase>)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public static readonly DependencyProperty HasPropertyProperty = DependencyProperty.Register("HasProperty", typeof(bool), typeof(PersonDataGrid));

		public bool HasProperty
		{
			get { return (bool)GetValue(HasPropertyProperty); }
			set { SetValue(HasPropertyProperty, value); }
		}
		#endregion

		#region events

		public event SqliteObjectSelectedEventHandler SqliteObjectSelected;
		public event RoutedEventHandler NewButtonClick;
		public event SqliteObjectSelectedEventHandler DeleteConfirmed;

		private void OnDataGridDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (SqliteObjectSelected != null || MainGrid.SelectedItem == null)
				SqliteObjectSelected(sender, new SqliteObjectEventArgs((SqliteObject)MainGrid.SelectedItem));
		}

		private void OnNewClick(object sender, RoutedEventArgs e)
		{
			if (NewButtonClick != null)
				NewButtonClick(sender, e);
		}

		private void OnDeleteClick(object sender, RoutedEventArgs e)
		{
			if (DeleteConfirmed == null || MainGrid.SelectedItem == null)
				return;

			if (!MessageHelper.WantToContinue())
				return;

			DeleteConfirmed(sender, new SqliteObjectEventArgs((SqliteObject)MainGrid.SelectedItem));
		}

		public delegate void SqliteObjectSelectedEventHandler(object sender, SqliteObjectEventArgs e);

		public class SqliteObjectEventArgs : EventArgs
		{
			public SqliteObject SqliteObject;

			public SqliteObjectEventArgs(SqliteObject sqliteObject)
			{
				SqliteObject = sqliteObject;
			}
		}

		#endregion

		private void OnAddProperty(object sender, RoutedEventArgs e)
		{
			if (!(MainGrid.SelectedItem is Owner))
				return;

			Owner owner = (Owner)MainGrid.SelectedItem;

			var properties = MainWindow.Context.Properties.Where(x => owner.Properties.All(y => x.ID != y.ID)).ToList();

			if (properties.Count == 0)
			{
				MessageBox.Show("No hay elementos", "Lista vacía", MessageBoxButton.OK, MessageBoxImage.Information);
				return;
			}

			SelectObjectWindow window = new SelectObjectWindow(properties);
			window.ShowDialog();

			if (!(window.SelectedObject is Property))
				return;

			Property property = (Property)window.SelectedObject;

			owner.AddProperty(property.ID);

			PropertiesGrid.ItemsSource = null;
			PropertiesGrid.ItemsSource = owner.Properties;
		}

		private void OnRemoveProperty(object sender, RoutedEventArgs e)
		{
			if (!(MainGrid.SelectedItem is Owner) || !(PropertiesGrid.SelectedItem is Property))
				return;

			if (!MessageHelper.WantToContinue())
				return;

			Owner owner = (Owner)MainGrid.SelectedItem;
			Property property = (Property)PropertiesGrid.SelectedItem;

			owner.RemoveProperty(property.ID);

			PropertiesGrid.ItemsSource = null;
			PropertiesGrid.ItemsSource = owner.Properties;
		}
	}
}
