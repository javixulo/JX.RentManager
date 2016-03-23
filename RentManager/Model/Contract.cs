using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using JX.SQLiteFramework;

namespace JX.RentManager.Model
{
	[SqliteTable(TableName = "contrato")]
	public class Contract : SqliteObject
	{
		[SqliteColumn(ColumnName = "id", IsKey = true, Description = "ID", Type = DbType.Int64, ReadOnly = true)]
		public Int64 ID { get; set; }

		[SqliteColumn(ColumnName = "codigo", Description = "Código")]
		public string Code { get; set; }

		[SqliteColumn(ColumnName = "inmueble", Description = "Inmueble", Type = DbType.Int64)]
		public Int64 Property { get; set; }

		[SqliteColumn(ColumnName = "inquilino", Description = "Inquilino")]
		public string Tenant { get; set; }

		[SqliteColumn(ColumnName = "fiador", Description = "Fiador", AllowNull = true)]
		public string Guarantor { get; set; }

		[SqliteColumn(ColumnName = "valor", Description = "Valor", Type = DbType.Double)]
		public double Value { get; set; }

		[SqliteColumn(ColumnName = "tipo", Description = "Tipo")]
		public string Type { get; set; }

		[SqliteColumn(ColumnName = "fecha_alta", Description = "Fecha de alta", Type = DbType.Date)]
		public DateTime CreationDate { get; set; }

		[SqliteColumn(ColumnName = "fecha_caducidad", Description = "Fecha de caducidad", Type = DbType.Date)]
		public DateTime ExpirationDate { get; set; }

		[SqliteColumn(ColumnName = "fecha_rev_renta", Description = "Fecha de revisión", Type = DbType.Date)]
		public DateTime RevisionDate { get; set; }

		[SqliteColumn(ColumnName = "fianza", Description = "Fianza", Type = DbType.Double)]
		public double Deposit { get; set; }

		[SqliteColumn(ColumnName = "meses_sin_renta", Description = "Meses sin renta", Type = DbType.Int64, DefaultValue = 0)]
		public Int64 MonthsWithoutRent { get; set; }

		[SqliteColumn(ColumnName = "forma_pago", Description = "Forma de pago", DefaultValue = "TRANSFERENCIA BANCARIA")]
		public string PaymentType { get; set; }

		[SqliteColumn(ColumnName = "estado", Description = "Estado", DefaultValue = "ACTIVO")]
		public string Status { get; set; }

		[SqliteColumn(ColumnName = "poliza_seguro", Description = "Póliza de seguro", AllowNull = true)]
		public string InsurancePolicy { get; set; }

		[SqliteColumn(ColumnName = "compania_seguro", Description = "Compañía de seguro", AllowNull = true)]
		public string InsuranceCompany { get; set; }

		[SqliteColumn(ColumnName = "capital_continente", Description = "Capital continente", AllowNull = true, Type = DbType.Double)]
		public double ContinentCapital { get; set; }

		[SqliteColumn(ColumnName = "franquicia", Description = "Franquicia", AllowNull = true)]
		public string Franchise { get; set; }

		[SqliteColumn(ColumnName = "fecha_vencimiento_seguro", AllowNull = true, Description = "Fecha de vencimiento del seguro", Type = DbType.Date)]
		public DateTime InsuranceExpirationDate { get; set; }

		[SqliteColumn(ColumnName = "observaciones_seguro", AllowNull = true, Description = "Observaciones seguro")]
		public string InsuranceObservations { get; set; }

		[SqliteColumn(ColumnName = "copia_local", AllowNull = true, Description = "Copia local")]
		public string LocalCopy { get; set; }

		[SqliteColumn(ColumnName = "observaciones", AllowNull = true, Description = "Observaciones")]
		public string Observations { get; set; }

		public Contract()
		{
			Property = -1;
		}

		public ObservableCollection<Receipt> Receipts
		{
			get { return new ObservableCollection<Receipt>(MainWindow.Context.Receipts.Where(x => x.Contract == ID)); }
		}

		public Property PropertyObject
		{
			get { return MainWindow.Context.Properties.First(x => x.ID == Property); }
		}

		//public override List<InputValuesControl.InputItem> GetAsInputItem(bool isNew)
		//{
		//	List<ComboBoxItem> comboBoxItems = new List<ComboBoxItem>();
		//	comboBoxItems.Add(new ComboBoxItem { Content = "Activo", Tag = "ACTIVO" });
		//	comboBoxItems.Add(new ComboBoxItem { Content = "Dado de baja", Tag = "DADO DE BAJA" });
		//	comboBoxItems.Add(new ComboBoxItem { Content = "Vencido", Tag = "VENCIDO" });
		//	comboBoxItems.Add(new ComboBoxItem { Content = "Interrumpido", Tag = "INTERRUMPIDO" });

		//	ComboBoxItem selected = comboBoxItems.FirstOrDefault(x => (string)x.Tag == Status);
		//	if (selected == null)
		//		comboBoxItems[0].IsSelected = true;
		//	else
		//		selected.IsSelected = true;

		//	var items = base.GetAsInputItem(isNew);

		//	var inputItem = items.First(x => x.Id == "Status");
		//	inputItem.DataType = typeof(IEnumerable<ComboBoxItem>);
		//	inputItem.Value = comboBoxItems;

		//	return items;
		//}
	}
}
