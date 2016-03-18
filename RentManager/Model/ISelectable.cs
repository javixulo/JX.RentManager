using System.Collections.Generic;

namespace RentManager.Model
{
	public interface ISelectable
	{
		Dictionary<string, string> GetDataGridColumns();
	}
}