using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Controls;
using JXWPFToolkit.Windows;
using RentManager.Model;
using SQLiteFramework;

namespace RentManager
{
	public class RentManagerDataContext : INotifyPropertyChanged
	{
		public List<Owner> Owners { get; set; }
		public List<Tenant> Tenants { get; set; }
		public List<Guarantor> Guarantors { get; set; }
		public List<Property> Properties { get; set; }
		public List<Contract> Contracts { get; set; }
		public List<Receipt> Receipts { get; set; }
		public List<Payment> Payments { get; set; }

		private SqliteObject _currentObject;
		private bool _isNew;
		public static SQLiteConnection DBConnection;

		public RentManagerDataContext()
		{
			DBConnection = new SQLiteConnection(@"Data Source=..\..\DB\RentManagerDBFile.db;Version=3;");
			DBConnection.Open();

			try
			{
				Load();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public void Load()
		{
			Owners = SqliteObject.GetAllElements<Owner>(DBConnection);
			Properties = SqliteObject.GetAllElements<Property>(DBConnection);
			Tenants = SqliteObject.GetAllElements<Tenant>(DBConnection);
			Guarantors = SqliteObject.GetAllElements<Guarantor>(DBConnection);
			Contracts = SqliteObject.GetAllElements<Contract>(DBConnection);
			Receipts = SqliteObject.GetAllElements<Receipt>(DBConnection);
			Payments = SqliteObject.GetAllElements<Payment>(DBConnection);

			NotifyPropertyChanged("Owners");
			NotifyPropertyChanged("Properties");
			NotifyPropertyChanged("Tenants");
			NotifyPropertyChanged("Guarantors");
			NotifyPropertyChanged("Contracts");
			NotifyPropertyChanged("Receipts");
			NotifyPropertyChanged("Payments");
		}

		public void Close()
		{
			DBConnection.Close();
		}

		public void Edit(SqliteObject sqliteObject)
		{
			_currentObject = sqliteObject;
			InputValuesWindow window = new InputValuesWindow(sqliteObject.GetAsInputItem(_isNew), WindowSize.Large, Orientation.Vertical, 2);
			window.InputFinished += OnInputFinished;
			window.Show();
		}

		public void New(SqliteObject sqliteObject)
		{
			_isNew = true;
			Edit(sqliteObject);
		}

		private void OnInputFinished(object sender, InputValuesWindow.InputFinishedEventArgs inputFinishedEventArgs)
		{
			_currentObject.SetValues(inputFinishedEventArgs.Items);
			int rowsAffected = _currentObject.Save(DBConnection, _isNew);

			if (rowsAffected == 1)
				Load();

			_isNew = false;
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion

		public void Delete(SqliteObject sqliteObject)
		{
			int rowsAffected = sqliteObject.Delete(DBConnection);

			if (rowsAffected == 1)
				Load();
		}
	}
}
