using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using JX.SQLiteFramework;

namespace JX.RentManager.Model
{
	[SqliteTable(TableName = "propietario")]
	public class Owner : PersonBase, IAccountOwner
	{
		public List<Property> Properties
		{
			get
			{
				List<Property> elements = new List<Property>();

				string sql = string.Format("select * FROM Inmueble WHERE id IN (SELECT inmueble FROM PropietarioInmueble WHERE propietario='{0}')", DNI);
				SQLiteCommand command = new SQLiteCommand(sql, RentManagerDataContext.DBConnection);

				using (SQLiteDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Property property = new Property();
						property.FillInstanceFromReader(reader);
						elements.Add(property);
					}
				}

				return elements;
			}
		}

		public int AddProperty(Int64 property)
		{
			string sql = "INSERT INTO PropietarioInmueble (inmueble, propietario) VALUES (:inmueble, :propietario)";
			using (SQLiteCommand command = new SQLiteCommand(sql, RentManagerDataContext.DBConnection))
			{
				command.Parameters.Add("inmueble", DbType.Int64).Value = property;
				command.Parameters.Add("propietario", DbType.String).Value = DNI;

				return command.ExecuteNonQuery();
			}
		}

		public int RemoveProperty(Int64 property)
		{
			string sql = "DELETE FROM PropietarioInmueble WHERE propietario = :propietario AND inmueble = :inmueble";
			using (SQLiteCommand command = new SQLiteCommand(sql, RentManagerDataContext.DBConnection))
			{
				command.Parameters.Add("inmueble", DbType.Int64).Value = property;
				command.Parameters.Add("propietario", DbType.String).Value = DNI;

				return command.ExecuteNonQuery();
			}
		}

		public ObservableCollection<string> Accounts
		{
			get
			{
				ObservableCollection<string> elements = new ObservableCollection<string>();

				string sql = string.Format("select numero FROM CuentaCorrientePropietario WHERE propietario='{0}'", DNI);
				SQLiteCommand command = new SQLiteCommand(sql, RentManagerDataContext.DBConnection);

				using (SQLiteDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						string value = (string)reader[0];
						elements.Add(value);
					}
				}

				return elements;
			}
		}

		public int AddAccount(string account)
		{
			const string sql = "INSERT INTO CuentaCorrientePropietario (numero, propietario) VALUES (:numero, :propietario)";
			int rowsAffected;
			using (SQLiteCommand command = new SQLiteCommand(sql, RentManagerDataContext.DBConnection))
			{
				command.Parameters.Add("numero", DbType.String).Value = account;
				command.Parameters.Add("propietario", DbType.String).Value = DNI;

				rowsAffected = command.ExecuteNonQuery();
			}

			NotifyPropertyChanged("Accounts");

			return rowsAffected;
		}

		public int RemoveAccount(string account)
		{
			const string sql = "DELETE FROM CuentaCorrientePropietario WHERE numero = :numero AND propietario = :propietario";
			int rowsAffected;
			using (SQLiteCommand command = new SQLiteCommand(sql, RentManagerDataContext.DBConnection))
			{
				command.Parameters.Add("numero", DbType.String).Value = account;
				command.Parameters.Add("propietario", DbType.String).Value = DNI;

				rowsAffected = command.ExecuteNonQuery();
			}

			NotifyPropertyChanged("Accounts");

			return rowsAffected;
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
