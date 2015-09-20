using System;
using System.Windows;
using System.Windows.Input;
using VectorToXamlConvertor.ViewModel;

namespace VectorToXamlConvertor.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModel = (MainWindowViewModel)TryFindResource("vm");
            //Initialize child viewmodels
            viewModel.InputViewModel = inputView.ViewModel;
            viewModel.PostConversionViewModel = postConversionScreen.ViewModel;
        }
    }
}
