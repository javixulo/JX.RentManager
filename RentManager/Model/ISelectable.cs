using System.Collections.Generic;

namespace JX.RentManager.Model
{
	public interface ISelectable
	{
		Dictionary<string, string> GetDataGridColumns();
	}
}