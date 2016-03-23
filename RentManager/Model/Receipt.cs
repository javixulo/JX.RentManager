using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using JX.WPFToolkit.Controls;
using JX.SQLiteFramework;

namespace JX.RentManager.Model
{
	[SqliteTable(TableName = "recibo")]
	public class Receipt : SqliteObject
	{
		[SqliteColumn(ColumnName = "id", IsKey = true, Description = "ID", Type = DbType.Int64, ReadOnly = true)]
		public Int64 ID { get; set; }

		[SqliteColumn(ColumnName = "numero", Description = "Número", Type = DbType.Int64)]
		public Int64 Number { get; set; }

		[SqliteColumn(ColumnName = "estado", Description = "Estado", DefaultValue = "PENDIENTE DE PAGO", ReadOnly = true)]
		public string Status { get; set; }

		[SqliteColumn(ColumnName = "propietario", Description = "Propietario")]
		public string Owner { get; set; }

		[SqliteColumn(ColumnName = "contrato", Description = "Contrato", Type = DbType.Int64, ReadOnly = true)]
		public Int64 Contract { get; set; }

		[SqliteColumn(ColumnName = "renta", Description = "Renta", Type = DbType.Double)]
		public double Rental { get; set; }

		[SqliteColumn(ColumnName = "IVA", Description = "IVA", Type = DbType.Double)]
		public double IVA { get; set; }

		[SqliteColumn(ColumnName = "IRPF", Description = "IRPF", Type = DbType.Double)]
		public double IRPF { get; set; }

		[SqliteColumn(ColumnName = "suplidos", Description = "Suplidos", AllowNull = true, Type = DbType.Double)]
		public double Supplied { get; set; }

		[SqliteColumn(ColumnName = "descuento_temp", Description = "Descuento temporal", AllowNull = true, Type = DbType.Double)]
		public double TemporaryDisccount { get; set; }

		[SqliteColumn(ColumnName = "importe", Description = "Importe", Type = DbType.Double)]
		public double Amount { get; set; }

		[SqliteColumn(ColumnName = "resto_por_pagar", Description = "Resto por pagar", AllowNull = true, Type = DbType.Double, ReadOnly = true)]
		public double RemainingToPay { get; set; }

		[SqliteColumn(ColumnName = "observaciones", AllowNull = true, Description = "Observaciones")]
		public string Observations { get; set; }

		[SqliteColumn(ColumnName = "fecha_emision", Description = "Fecha de emisión", Type = DbType.Date)]
		public DateTime IssueDate { get; set; }

		public Contract ContractObject
		{
			get { return MainWindow.Context.Contracts.First(x => x.ID == Contract); }
		}

		public ObservableCollection<Payment> Payments
		{
			get { return new ObservableCollection<Payment>(MainWindow.Context.Payments.Where(x => x.Receipt == ID)); }
		}

		public override List<InputValuesControl.InputItem> GetAsInputItem(bool isNew)
		{
			List<ComboBoxItem> ownerItems = new List<ComboBoxItem>();

			foreach (Owner owner in ContractObject.PropertyObject.Owners)
			{
				ComboBoxItem item = new ComboBoxItem { Content = string.Format("{0} - {1}", owner.DNI, owner.Name), Tag = owner.DNI };
				if (owner.DNI == Owner)
					item.IsSelected = true;
				ownerItems.Add(item);
			}

			var items = base.GetAsInputItem(isNew);

			var inputItem = items.First(x => x.Id == "Owner");
			inputItem.DataType = typeof(IEnumerable<ComboBoxItem>);
			inputItem.Value = ownerItems;

			return items;
		}

	}
}
