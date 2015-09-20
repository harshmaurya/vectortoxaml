using System.ComponentModel;
using System.Runtime.CompilerServices;
using VectorToXamlConvertor.Annotations;

namespace VectorToXamlConvertor
{
    public class ConversionSettings : INotifyPropertyChanged
    {
        private string _outputDirectory;
        private BrushWrapper _autoFill;
        private bool _xamlOutputEnabled;
        private bool _pathOutputEnabled;
        private bool _isPreviewEnabled;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        public string OutputDirectory
        {
            get { return _outputDirectory; }
            set
            {
                _outputDirectory = value;
                OnPropertyChanged();
            }
        }

        public BrushWrapper AutoFill
        {
            get { return _autoFill; }
            set
            {
                _autoFill = value;
                OnPropertyChanged();
            }
        }

        public bool XamlOutputEnabled
        {
            get { return _xamlOutputEnabled; }
            set
            {
                _xamlOutputEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool PathOutputEnabled
        {
            get { return _pathOutputEnabled; }
            set
            {
                _pathOutputEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool IsPreviewEnabled
        {
            get { return _isPreviewEnabled; }
            set
            {
                _isPreviewEnabled = value;
                OnPropertyChanged();
            }
        }

        public void CopyFrom(ConversionSettings source)
        {
            OutputDirectory = source.OutputDirectory;
            XamlOutputEnabled = source.XamlOutputEnabled;
            PathOutputEnabled = source.PathOutputEnabled;
            IsPreviewEnabled = source.IsPreviewEnabled;
            AutoFill = source.AutoFill;
        }
    }
}
