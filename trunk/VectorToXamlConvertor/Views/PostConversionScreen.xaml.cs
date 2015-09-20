using System.Windows.Controls;
using VectorToXamlConvertor.ViewModel;

namespace VectorToXamlConvertor.Views
{
    /// <summary>
    /// Interaction logic for PostConversionScreen.xaml
    /// </summary>
    public partial class PostConversionScreen : UserControl
    {
        public PostConversionScreen()
        {
            InitializeComponent();
        }

        public PostConversionScreenViewModel ViewModel
        {
            get { return Resources["ViewModel"] as PostConversionScreenViewModel; }
            set { Resources["ViewModel"] = value; }
        }
    }
}
