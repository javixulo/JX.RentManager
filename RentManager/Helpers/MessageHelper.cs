using System.Windows;

namespace JX.RentManager.Helpers
{
	public static class MessageHelper
	{
		public static bool WantToContinue()
		{
			return MessageBox.Show("¿Estás seguro?", "Eliminar", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK;
		}
	}
}
