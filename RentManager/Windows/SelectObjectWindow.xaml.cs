using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using RentManager.Model;
using SQLiteFramework;

namespace RentManager.Windows
{

	public partial class SelectObjectWindow : Window
	{
		public SqliteObject SelectedObject { get; private set; }

		public SelectObjectWindow(IEnumerable<ISelectable> objects)
		{
			InitializeComponent();

			var sqliteObjects = objects as IList<ISelectable> ?? objects.ToList();
			var columns = sqliteObjects.First().GetDataGridColumns();

			foreach (var column in columns)
				MainGrid.Columns.Add(new DataGridTextColumn { Header = column.Value, Binding = new Binding(column.Key) });

			MainGrid.ItemsSource = sqliteObjects;
		}


		public static readonly DependencyProperty ItemsSourceProperty = ItemsControl.ItemsSourceProperty.AddOwner(typeof(SelectObjectWindow));

		public IEnumerable ItemsSource
		{
			get { return (IEnumerable)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		private void OnDataGridMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			SelectedObject = (SqliteObject) MainGrid.SelectedItem;
			Close();
		}
	}
}
