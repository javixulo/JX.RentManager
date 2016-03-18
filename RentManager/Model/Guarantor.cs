using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using SQLiteFramework;

namespace RentManager.Model
{
	[SqliteTable(TableName = "fiador")]
	public class Guarantor : PersonBase, IAccountOwner
	{
		public ObservableCollection<string> Accounts
		{
			get
			{
				ObservableCollection<string> elements = new ObservableCollection<string>();

				string sql = string.Format("select numero FROM CuentaCorrienteFiador WHERE fiador='{0}'", DNI);
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
			const string sql = "INSERT INTO CuentaCorrienteFiador (numero, fiador) VALUES (:numero, :fiador)";
			int rowsAffected;
			using (SQLiteCommand command = new SQLiteCommand(sql, RentManagerDataContext.DBConnection))
			{
				command.Parameters.Add("numero", DbType.String).Value = account;
				command.Parameters.Add("fiador", DbType.String).Value = DNI;

				rowsAffected = command.ExecuteNonQuery();
			}

			NotifyPropertyChanged("Accounts");

			return rowsAffected;
		}

		public int RemoveAccount(string account)
		{
			const string sql = "DELETE FROM CuentaCorrienteFiador WHERE numero = :numero AND fiador = :fiador";
			int rowsAffected;
			using (SQLiteCommand command = new SQLiteCommand(sql, RentManagerDataContext.DBConnection))
			{
				command.Parameters.Add("numero", DbType.String).Value = account;
				command.Parameters.Add("fiador", DbType.String).Value = DNI;

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
