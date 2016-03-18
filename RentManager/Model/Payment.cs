using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using JXWPFToolkit.Controls;
using SQLiteFramework;

namespace RentManager.Model
{
	[SqliteTable(TableName = "pago")]
	public class Payment : SqliteObject
	{
		[SqliteColumn(ColumnName = "id", IsKey = true, Description = "ID", Type = DbType.Int64, ReadOnly = true)]
		public Int64 ID { get; set; }

		[SqliteColumn(ColumnName = "recibo", Description = "Recibo", Type = DbType.Int64, ReadOnly = true)]
		public Int64 Receipt { get; set; }

		[SqliteColumn(ColumnName = "importe", Description = "Importe", Type = DbType.Double)]
		public double Amount { get; set; }

		[SqliteColumn(ColumnName = "forma_pago", Description = "Forma de pago", DefaultValue = "TRANSFERENCIA BANCARIA")]
		public string PaymentType { get; set; }

		[SqliteColumn(ColumnName = "vencimiento_pagare_giro", Description = "Fecha de vencimiento", AllowNull = true, Type = DbType.Date)]
		public DateTime ExpirationDate { get; set; }

		[SqliteColumn(ColumnName = "gastos_impago_pagare", Description = "Gastos de impago", AllowNull = true, Type = DbType.Double)]
		public double NonPaymentExpenses { get; set; }

		[SqliteColumn(ColumnName = "nombre_banco_transfer_giro", Description = "Nombre de banco", AllowNull = true)]
		public string BankName { get; set; }

		[SqliteColumn(ColumnName = "numero_cuenta_transfer_giro", Description = "CCC", AllowNull = true)]
		public string BankAccount { get; set; }

		[SqliteColumn(ColumnName = "numero_recibo_giro", Description = "Número de recibo", AllowNull = true)]
		public string ReceiptNumber { get; set; }

		[SqliteColumn(ColumnName = "localidad_expedicion_giro", Description = "Localidad de expedición", AllowNull = true)]
		public string ExpeditionPlace { get; set; }

		[SqliteColumn(ColumnName = "fecha_expedicion_giro", Description = "Fecha de expedición", AllowNull = true, Type = DbType.Date)]
		public DateTime ExpeditionnDate { get; set; }

		[SqliteColumn(ColumnName = "oficina_giro", Description = "Oficina de giro", AllowNull = true)]
		public string DraftOffice { get; set; }

		[SqliteColumn(ColumnName = "nombre_librado_giro", Description = "Nombre del librado", AllowNull = true)]
		public string DraftName { get; set; }

		[SqliteColumn(ColumnName = "domicilio_librado_giro", Description = "Domicilio del librado", AllowNull = true)]
		public string DraftAddress { get; set; }

		[SqliteColumn(ColumnName = "observaciones", AllowNull = true, Description = "Observaciones")]
		public string Observations { get; set; }

		[SqliteColumn(ColumnName = "fecha", Description = "Fecha", Type = DbType.Date)]
		public DateTime Date { get; set; }


		public override List<InputValuesControl.InputItem> GetAsInputItem(bool isNew)
		{
			List<ComboBoxItem> comboBoxItems = new List<ComboBoxItem>();	
			comboBoxItems.Add( new ComboBoxItem { Content = "Transferencia bancaria", Tag = "TRANSFERENCIA BANCARIA", IsSelected = true});
			comboBoxItems.Add( new ComboBoxItem { Content = "Pagaré", Tag = "PAGARE"});
			comboBoxItems.Add(new ComboBoxItem { Content = "Efectivo", Tag = "EFECTIVO" });
			comboBoxItems.Add(new ComboBoxItem { Content = "Giro domiciliado", Tag = "GIRO DOMICILIADO" });
			
			var items = base.GetAsInputItem(isNew);

			var inputItem = items.First(x => x.Id == "PaymentType");
			inputItem.DataType = typeof(IEnumerable<ComboBoxItem>);
			inputItem.Value = comboBoxItems;

			return items;
		}
	}
}
