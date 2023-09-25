using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace FileDiffCompare
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<TabItemViewModel> Tabs { get; set; }

        public ICommand NewTabCommand { get; set; }
        public ICommand AboutCommand { get; set; }

        public MainWindowViewModel()
        {
            Tabs = new ObservableCollection<TabItemViewModel>();
            NewTabCommand = new RelayCommand(AddNewTab);
            AboutCommand = new RelayCommand(DisplayAbout);
        }

        private void AddNewTab(object _)
        {
            var newTab = new TabItemViewModel
            {
                Title = $"Tab {Tabs.Count + 1}"
                //... set other properties as needed 
            };
            Tabs.Add(newTab);
        }

        private void DisplayAbout(object _)
        {
            MessageBox.Show("FileDiffCompare - Created by YourName. Version 1.0.0");
        }

        private string _someProperty;
        public string SomeProperty
        {
            get { return _someProperty; }
            set
            {
                if (_someProperty != value)
                {
                    _someProperty = value;
                    OnPropertyChanged(nameof(SomeProperty));
                }
            }
        }

        // ... other properties and methods ...
    }
}
