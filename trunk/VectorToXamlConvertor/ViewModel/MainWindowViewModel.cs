using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VectorToXamlConvertor.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public PostConversionScreenViewModel PostConversionViewModel
        {
            get { return _postConversionViewModel; }
            set
            {
                _postConversionViewModel = value;
                _postConversionViewModel.GotoHomeEvent += PostConversionViewModelGotoHome;
            }
        }

        void PostConversionViewModelGotoHome(object sender, System.EventArgs e)
        {
            InputViewModel.ResetView();
            PostConversionViewVisible = false;
        }

        public InputScreenViewModel InputViewModel
        {
            get { return _inputViewModel; }
            set
            {
                _inputViewModel = value;
                _inputViewModel.ConversionComplete += InputViewModelConversionComplete;
            }
        }

        public bool PostConversionViewVisible
        {
            get { return _postConversionViewVisible; }
            set
            {
                _postConversionViewVisible = value;
                OnPropertyChanged();
            }
        }

        void InputViewModelConversionComplete(object sender, ConversionEventArgs e)
        {
            if (!InputViewModel.ConversionSettings.IsPreviewEnabled)
            {
                PostConversionViewModel.WriteOutput(InputViewModel.ConversionSettings.XamlOutputEnabled, InputViewModel.ConversionSettings.PathOutputEnabled, InputViewModel.ConversionSettings.OutputDirectory, e.ConvertedObjects);
                return;
            }
            PostConversionViewModel.ResetView();
            foreach (var vector in e.ConvertedObjects)
            {
                PostConversionViewModel.ConvertedObjects.Add(vector);
            }

            PostConversionViewModel.ConversionSettings.CopyFrom(InputViewModel.ConversionSettings);
            PostConversionViewModel.SelectedVector = PostConversionViewModel.ConvertedObjects[0];
            PostConversionViewVisible = true;
        }

        private PostConversionScreenViewModel _postConversionViewModel;
        private InputScreenViewModel _inputViewModel;
        private bool _postConversionViewVisible;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
