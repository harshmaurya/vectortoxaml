using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Common.Themes;
using VectorToXamlConvertor.Convertor;
using VectorToXamlConvertor.Services;
using VectorToXamlConvertor.Views;
using Application = System.Windows.Application;

namespace VectorToXamlConvertor.ViewModel
{
    public class ConversionEventArgs : EventArgs
    {
        public ConversionEventArgs(IList<VectorXaml> convertedObjects)
        {
            ConvertedObjects = convertedObjects;
        }

        public IList<VectorXaml> ConvertedObjects { get; private set; }
    }

    public class InputScreenViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ICommand _convertCommand;
        private ICommand _addFilesCommand;
        private ICommand _clearCommand;
        private ICommand _exitCommmand;
        private ICommand _changeThemeCommmand;
        private ICommand _changeOutPutDirectoryCommand;
        private bool _conversionStarted;
        private bool _inputFilesAdded;
        private readonly Dispatcher UIDispatcher;
        private readonly List<IEnumerable<string>> _convertedObjects = new List<IEnumerable<string>>();

        public event EventHandler<ConversionEventArgs> ConversionComplete;

        public InputScreenViewModel()
        {
            UIDispatcher = Dispatcher.CurrentDispatcher;
            Input = new ObservableCollection<string>();
            AutoFillBrushes = new ObservableCollection<BrushWrapper> { new BrushWrapper("Black", Brushes.Black), new BrushWrapper("Red", Brushes.Red), new BrushWrapper("Blue", Brushes.Blue), new BrushWrapper("Green", Brushes.Green) };
            ConversionSettings = new ConversionSettings { AutoFill = AutoFillBrushes[0], XamlOutputEnabled = true, PathOutputEnabled = true, IsPreviewEnabled = true };
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RaiseConversionComplete(IList<VectorXaml> convertedObjects)
        {
            var handler = ConversionComplete;
            if (handler != null)
            {
                handler(this, new ConversionEventArgs(convertedObjects));
            }
        }

        public ConversionSettings ConversionSettings { get; set; }

        public ObservableCollection<string> Input { get; set; }

        public ICommand AddFilesCommand
        {
            get { return _addFilesCommand ?? (_addFilesCommand = new RelayCommand(AddFilesFromDialog)); }
        }

        public ICommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new RelayCommand(ResetView));
            }
        }

        public ICommand ExitCommand
        {
            get { return _exitCommmand ?? (_exitCommmand = new RelayCommand(() => Application.Current.Shutdown())); }
        }

        public ICommand ChangeThemeCommand
        {
            get { return _changeThemeCommmand ?? (_changeThemeCommmand = new RelayCommand(ChangeTheme)); }
        }

        public ICommand ChangeOutPutDirectoryCommand
        {
            get
            {
                return _changeOutPutDirectoryCommand ??
                       (_changeOutPutDirectoryCommand = new RelayCommand(ChangeOutPutDirectory));
            }
        }

        internal void ResetView()
        {
            Input.Clear();
            InputFilesAdded = false;
            ConversionSettings.OutputDirectory = String.Empty;
            ConversionSettings.IsPreviewEnabled = true;
            ConversionSettings.PathOutputEnabled = true;
            ConversionSettings.XamlOutputEnabled = true;
            _convertedObjects.Clear();
        }

        private void ChangeOutPutDirectory()
        {
            var openFileDialog = new FolderBrowserDialog();
            openFileDialog.ShowDialog();
            ConversionSettings.OutputDirectory = openFileDialog.SelectedPath;
        }

        private static void ChangeTheme()
        {
            ThemesManager.Instance.CurrentTheme = ThemesManager.Instance.CurrentTheme == ThemeNames.MauryaDark ? ThemeNames.MauryaLight : ThemeNames.MauryaDark;
        }

        private void AddFilesFromDialog()
        {
            var files = FileDialogService.ShowFileDialog(true);
            AddFiles(files);
        }

        public void AddFiles(IList<string> files)
        {
            bool allValidFiles = true;
            foreach (var file in files)
            {
                if (ValidateFile(file))
                    Input.Add(file);
                else
                {
                    allValidFiles = false;
                }
            }
            if (files.Count > 0 && allValidFiles)
            {
                InputFilesAdded = true;
                ConversionSettings.OutputDirectory = Directory.GetParent(files[0]).FullName;
            }

            if (!allValidFiles)
            {
                MessageService.ShowMessage(String.Format("Some file(s) are not valid. Only vector files (.svg) are allowed"));
            }
        }

        private bool ValidateFile(string file)
        {
            if (!file.EndsWith("svg"))
            {
                return false;
            }
            return true;
        }

        public bool InputFilesAdded
        {
            get { return _inputFilesAdded; }
            set
            {
                _inputFilesAdded = value;
                OnPropertyChanged();
            }
        }

        public ICommand ConvertCommand
        {
            get { return _convertCommand ?? (_convertCommand = new RelayCommand(ConvertAsync)); }
        }

        private void ConvertAsync()
        {
            //Need STA thread so can't use threadpool
            var staThread = new Thread(Convert);
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            //Convert(null);
        }

        private void Convert()
        {
            if (Input.Count == 0)
            {
                MessageService.ShowMessage("Please select some file(s) first");
                return;
            }
            ConversionStarted = true;
            foreach (var file in Input)
            {
                try
                {
                    var convertor = new SvgConvertor(file);
                    _convertedObjects.Add(convertor.Convert());
                }
                catch (Exception exception)
                {
                    MessageService.ShowMessage(exception.Message);
                }
            }
            ConversionStarted = false;
            UIDispatcher.Invoke(() => RaiseConversionComplete(_convertedObjects.Select((pathCollection) => SvgConvertor.GetVectorXaml(pathCollection, ConversionSettings.AutoFill.Brush)).ToList()));
        }
        public bool ConversionStarted
        {
            get { return _conversionStarted; }
            set
            {
                _conversionStarted = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BrushWrapper> AutoFillBrushes { get; set; }

        private ICommand _aboutCommmand;
        public ICommand AboutCommand
        {
            get
            {
                return _aboutCommmand ?? (_aboutCommmand = new RelayCommand(() =>
                                                                            {
                                                                                var aboutWindow = new AboutPage();
                                                                                aboutWindow.ShowDialog();
                                                                            }));
            }
        }
    }
}
