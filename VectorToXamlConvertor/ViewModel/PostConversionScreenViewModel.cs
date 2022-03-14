using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;
using VectorToXamlConvertor.Services;

namespace VectorToXamlConvertor.ViewModel
{
    public class PostConversionScreenViewModel : INotifyPropertyChanged
    {
        private ICommand _gotoHomeCommand;
        private ICommand _saveCommmand;

        public ICommand SaveCommmand
        {
            get { return _saveCommmand ?? (_saveCommmand = new ActionCommand(() => WriteOutput(ConversionSettings.XamlOutputEnabled, ConversionSettings.PathOutputEnabled, ConversionSettings.OutputDirectory, ConvertedObjects))); }
        }

        public ICommand GotoHomeCommand
        {
            get
            {
                return _gotoHomeCommand ?? (_gotoHomeCommand = new ActionCommand(RaiseGotoHome));
            }
        }

        private void RaiseGotoHome()
        {
            var handler = GotoHomeEvent;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
        public PostConversionScreenViewModel()
        {
            ConvertedObjects = new ObservableCollection<VectorXaml>();
            ConversionSettings = new ConversionSettings();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<VectorXaml> ConvertedObjects { get; set; }

        private VectorXaml _selectedVector;

        public VectorXaml SelectedVector
        {
            get { return _selectedVector; }
            set
            {
                _selectedVector = value;
                OnPropertyChanged();
            }
        }

        public ConversionSettings ConversionSettings { get; set; }

        public event EventHandler GotoHomeEvent;

        internal void ResetView()
        {
            ConvertedObjects.Clear();
            SelectedVector = null;
        }

        internal void WriteOutput(bool writeXaml, bool writePath, string directory, IList<VectorXaml> convertedObjects)
        {
            for (var i = 0; i < convertedObjects.Count; i++)
            {
                if (writeXaml)
                {
                    File.WriteAllText(Path.Combine(directory, i + "_xaml.xaml"), convertedObjects[i].PathGeometryXaml);
                }
                if (writePath)
                {
                    File.WriteAllText(Path.Combine(directory, i + "_path.txt"), convertedObjects[i].PathData);
                }
            }
            MessageService.ShowMessage("Files saved at " + directory);
        }

    }
}
