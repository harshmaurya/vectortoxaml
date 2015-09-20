using System.Windows;
using System.Windows.Controls;
using VectorToXamlConvertor.ViewModel;

namespace VectorToXamlConvertor.Views
{
    /// <summary>
    /// Interaction logic for InputView.xaml
    /// </summary>
    public partial class InputView : UserControl
    {
        public InputView()
        {
            InitializeComponent();
        }

        public InputScreenViewModel ViewModel
        {
            get { return Resources["ViewModel"] as InputScreenViewModel; }
            set
            {
                Resources["ViewModel"] = value;
            }
        }

        private void InputFilesContainerOnDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            ViewModel.AddFiles(files);
        }
    }
}
