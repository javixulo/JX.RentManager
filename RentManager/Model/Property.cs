using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Controls;
using JXWPFToolkit.Controls;
using SQLiteFramework;

namespace RentManager.Model
{
	[SqliteTable(TableName = "inmueble")]
	public class Property : SqliteObject, ISelectable
	{
		[SqliteColumn(ColumnName = "id", IsKey = true, Description = "ID", Type = DbType.Int64, ReadOnly = true)]
		public Int64 ID { get; set; }

		[SqliteColumn(ColumnName = "nombre", Description = "Nombre")]
		public string Name { get; set; }

		[SqliteColumn(ColumnName = "direccion", Description = "Dirección")]
		public string Address { get; set; }

		[SqliteColumn(ColumnName = "tipo", Description = "Tipo")]
		public string Type { get; set; }

		[SqliteColumn(ColumnName = "gastos_comunitarios", AllowNull = true, Description = "Gastos comunitarios", Type = DbType.Double)]
		public double CommunityExpenditure { get; set; }

		[SqliteColumn(ColumnName = "referencia_catastral", Description = "Referencia catastral")]
		public string ÇadastralReference { get; set; }

		[SqliteColumn(ColumnName = "ano_construccion", AllowNull = true, Description = "Año de construcción", Type = DbType.Int64)]
		public Int64 ConstructionYear { get; set; }

		[SqliteColumn(ColumnName = "ano_compra", AllowNull = true, Description = "Año de compra", Type = DbType.Int64)]
		public Int64 PurchaseYear { get; set; }

		[SqliteColumn(ColumnName = "hipoteca", AllowNull = true, Description = "Hipotecas", Type = DbType.Double)]
		public double Mortage { get; set; }

		[SqliteColumn(ColumnName = "telefono", AllowNull = true, Description = "Teléfono", Type = DbType.Double)]
		public double TelephoneExpenses { get; set; }

		[SqliteColumn(ColumnName = "agua", AllowNull = true, Description = "Agua", Type = DbType.Double)]
		public double WatereExpenses { get; set; }

		[SqliteColumn(ColumnName = "electricidad", AllowNull = true, Description = "Electricidad", Type = DbType.Double)]
		public double ElectricityeExpenses { get; set; }

		[SqliteColumn(ColumnName = "jardineria", AllowNull = true, Description = "Jardinería", Type = DbType.Double)]
		public double GardenExpenses { get; set; }

		[SqliteColumn(ColumnName = "gastos_varios", AllowNull = true, Description = "Gastos varios", Type = DbType.Double)]
		public double VariousExpenses { get; set; }

		[SqliteColumn(ColumnName = "plaza_parking", AllowNull = true, Description = "Plaza de parking", Type = DbType.Double)]
		public double ParkingLot { get; set; }

		[SqliteColumn(ColumnName = "parcela", AllowNull = true, Description = "Parcela")]
		public string Ground { get; set; }

		[SqliteColumn(ColumnName = "metros_construidos", AllowNull = true, Description = "Metros construidos")]
		public string BuiltMeters { get; set; }

		[SqliteColumn(ColumnName = "metros_utiles", AllowNull = true, Description = "Metros útiles")]
		public string UsefulMeters { get; set; }

		[SqliteColumn(ColumnName = "observaciones", AllowNull = true, Description = "Observaciones")]
		public string Observations { get; set; }

		[SqliteColumn(ColumnName = "fecha_alta", Description = "Fecha de alta", Type = DbType.Date)]
		public DateTime CreationDate { get; set; }

		public List<Owner> Owners
		{
			get
			{
				List<Owner> elements = new List<Owner>();

				string sql = string.Format("select * FROM Propietario WHERE dni_cif IN (SELECT propietario FROM PropietarioInmueble WHERE inmueble='{0}')", ID);
				SQLiteCommand command = new SQLiteCommand(sql, RentManagerDataContext.DBConnection);

				using (SQLiteDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						Owner owner = new Owner();
						owner.FillInstanceFromReader(reader);
						elements.Add(owner);
					}
				}

				return elements;
			}
		}

		public override List<InputValuesControl.InputItem> GetAsInputItem(bool isNew)
		{
			List<ComboBoxItem> comboBoxItems = new List<ComboBoxItem>();
			comboBoxItems.Add(new ComboBoxItem { Content = "Vivienda", Tag = "VIVIENDA" });
			comboBoxItems.Add(new ComboBoxItem { Content = "Bajo comercial", Tag = "BAJO COMERCIAL" });
			comboBoxItems.Add(new ComboBoxItem { Content = "Nave industrial", Tag = "NAVE INDUSTRIAL" });
			comboBoxItems.Add(new ComboBoxItem { Content = "Salón de celebraciones", Tag = "SALON DE CELEBRACIONES" });
			comboBoxItems.Add(new ComboBoxItem { Content = "Otro", Tag = "OTRO" });

			ComboBoxItem selected = comboBoxItems.FirstOrDefault(x => (string)x.Tag == Type);
			if (selected == null)
				comboBoxItems[0].IsSelected = true;
			else
				selected.IsSelected = true;

			var items = base.GetAsInputItem(isNew);

			var inputItem = items.First(x => x.Id == "Type");
			inputItem.DataType = typeof(IEnumerable<ComboBoxItem>);
			inputItem.Value = comboBoxItems;

			return items;
		}

		#region Implementation of ISelectable

		public Dictionary<string, string> GetDataGridColumns()
		{
			Dictionary<string, string> columns = new Dictionary<string, string>();

			columns.Add("Name", "Nombre");
			columns.Add("Address", "Dirección");
			columns.Add("Phone", "Teléfono");
			columns.Add("Type", "Tipo");

			return columns;
		}

		#endregion

	}
}
