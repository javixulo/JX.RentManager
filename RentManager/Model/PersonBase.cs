using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using JXWPFToolkit.Controls;
using RentManager.Controls;
using SQLiteFramework;

namespace RentManager.Model
{
	public abstract class PersonBase : SqliteObject, ISelectable
	{
		[SqliteColumn(ColumnName = "dni_cif", IsKey = true, AllowNull = false, Description = "DNI/CIF")]
		public string DNI { get; set; }

		[SqliteColumn(ColumnName = "nombre", AllowNull = false, Description = "Nombre")]
		public string Name { get; set; }

		[SqliteColumn(ColumnName = "direccion", AllowNull = false, Description = "Dirección")]
		public string Address { get; set; }

		[SqliteColumn(ColumnName = "telefono_1", AllowNull = false, Description = "Teléfono")]
		public string Phone1 { get; set; }

		[SqliteColumn(ColumnName = "telefono_2", AllowNull = true, Description = "Teléfono (2)")]
		public string Phone2 { get; set; }

		[SqliteColumn(ColumnName = "email", AllowNull = true, Description = "E-Mail")]
		public string Mail { get; set; }

		[SqliteColumn(ColumnName = "estado", AllowNull = false, Description = "Estado", DefaultValue = "DADO DE ALTA")]
		public string Status { get; set; }

		[SqliteColumn(ColumnName = "fecha_alta", AllowNull = false, Description = "Fecha de alta", Type = DbType.Date)]
		public DateTime CreationDate { get; set; }

		[SqliteColumn(ColumnName = "observaciones", AllowNull = true, Description = "Observaciones")]
		public string Observations { get; set; }

		public override List<InputValuesControl.InputItem> GetAsInputItem(bool isNew)
		{
			List<ComboBoxItem> comboBoxItems = new List<ComboBoxItem>();
			comboBoxItems.Add(new ComboBoxItem { Content = "Dado de alta", Tag = "DADO DE ALTA" });
			comboBoxItems.Add(new ComboBoxItem { Content = "Dado de baja", Tag = "DADO DE BAJA" });

			ComboBoxItem selected = comboBoxItems.FirstOrDefault(x => (string)x.Tag == Status);
			if (selected == null)
				comboBoxItems[0].IsSelected = true;
			else
				selected.IsSelected = true;

			var items = base.GetAsInputItem(isNew);

			var inputItem = items.First(x => x.Id == "Status");
			inputItem.DataType = typeof(IEnumerable<ComboBoxItem>);
			inputItem.Value = comboBoxItems;

			return items;
		}

		#region Implementation of ISelectable

		public Dictionary<string, string> GetDataGridColumns()
		{
			Dictionary<string, string> columns = new Dictionary<string, string>();

			columns.Add("DNI", "DNI/CIF");
			columns.Add("Name", "Nombre");

			return columns;
		}

		#endregion

	}
}
