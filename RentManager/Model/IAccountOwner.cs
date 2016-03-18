using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RentManager.Model
{
	public interface IAccountOwner : INotifyPropertyChanged
	{
		ObservableCollection<string> Accounts { get; }

		int AddAccount(string account);
		int RemoveAccount(string account);
	}
}