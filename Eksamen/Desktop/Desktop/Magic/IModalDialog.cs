using Desktop.ViewModels;
using Desktop.Views;
using System.Diagnostics;
using System.Windows;

namespace Desktop.Magic
{
    public interface IModalDialog
    {
        bool ShowDialog(string viewKey, IViewModel viewModel);
    }

    public class ModalDialogLocator : IModalDialog
    {
        public bool ShowDialog(string viewKey, IViewModel viewModel)
        {
            bool result = false;
            switch (viewKey)
            {
                case "Highscore":
                    Window dlg = new HighscoreSettings();
                    dlg.DataContext = viewModel;
                    dlg.ShowDialog();
                    break;
                default:
                    Debug.WriteLine("Unable to find viewKey: " + viewKey);
                    break;
            }
            return result;
        }
    }
}
