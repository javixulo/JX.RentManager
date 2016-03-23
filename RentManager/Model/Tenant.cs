using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using JX.SQLiteFramework;

namespace JX.RentManager.Model
{
	[SqliteTable(TableName = "inquilino")]
	public class Tenant : PersonBase, IAccountOwner
	{
		[SqliteColumn(ColumnName = "dia_pago", AllowNull = false, Description = "Día de pago", Type = DbType.Int64)]
		public Int64 PaymentDay { get; set; }

		public ObservableCollection<string> Accounts
		{
			get
			{
				ObservableCollection<string> elements = new ObservableCollection<string>();

				string sql = string.Format("select numero FROM CuentaCorrienteInquilino WHERE inquilino='{0}'", DNI);
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
			const string sql = "INSERT INTO CuentaCorrienteInquilino (numero, inquilino) VALUES (:numero, :inquilino)";
			int rowsAffected;
			using (SQLiteCommand command = new SQLiteCommand(sql, RentManagerDataContext.DBConnection))
			{
				command.Parameters.Add("numero", DbType.String).Value = account;
				command.Parameters.Add("inquilino", DbType.String).Value = DNI;

				rowsAffected = command.ExecuteNonQuery();
			}

			NotifyPropertyChanged("Accounts");

			return rowsAffected;
		}

		public int RemoveAccount(string account)
		{
			const string sql = "DELETE FROM CuentaCorrienteInquilino WHERE numero = :numero AND inquilino = :inquilino";
			int rowsAffected;
			using (SQLiteCommand command = new SQLiteCommand(sql, RentManagerDataContext.DBConnection))
			{
				command.Parameters.Add("numero", DbType.String).Value = account;
				command.Parameters.Add("inquilino", DbType.String).Value = DNI;

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
