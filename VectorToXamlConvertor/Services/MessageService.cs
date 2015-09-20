using System.Windows;

namespace VectorToXamlConvertor.Services
{
    static class MessageService
    {
        internal static void ShowMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }
    }
}
