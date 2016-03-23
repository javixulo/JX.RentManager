using System;
using System.Data;
using JX.SQLiteFramework;

namespace JX.RentManager.Model
{
	[SqliteTable(TableName = "LineaLibroMayor")]
	public class LedgerLine
	{
		[SqliteColumn(ColumnName = "numero", IsKey = true, Description = "Número", Type = DbType.Int32, ReadOnly = true)]
		public int NBumber { get; set; }

		[SqliteColumn(ColumnName = "inquilino", IsKey = true, Description = "Inquilino", ReadOnly = true)]
		public string Tenant { get; set; }

		[SqliteColumn(ColumnName = "concepto", Description = "Concepto", AllowNull = true)]
		public string Concept { get; set; }

		[SqliteColumn(ColumnName = "fecha", Description = "Fecha", Type = DbType.Date)]
		public DateTime Date { get; set; }

		[SqliteColumn(ColumnName = "debe", Description = "Debe", AllowNull = true, Type = DbType.Double, DefaultValue = 0)]
		public double Debit { get; set; }

		[SqliteColumn(ColumnName = "haber", Description = "Haber", AllowNull = true, Type = DbType.Double, DefaultValue = 0)]
		public double Credit { get; set; }
	}
}
