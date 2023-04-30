using DiplomMVVM.Core;
using System.Windows;

namespace DiplomMVVM.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand ParametersViewCommand { get; set; }
        public RelayCommand BuldozersViewCommand { get; set; }
        public RelayCommand ReportsViewCommand { get; set; }
        public RelayCommand CloseAppCommand { get; set; }
        public ParametersViewModel ParametersVM { get; set; }
        public BuldozersViewModel BuldozersVM { get; set; }
        public ReportsViewModel ReportsVM { get; set; }
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            ParametersVM = new ParametersViewModel();
            BuldozersVM = new BuldozersViewModel();
            ReportsVM = new ReportsViewModel();
            ParametersViewCommand = new RelayCommand(o =>
            {
                CurrentView = ParametersVM;
            });
            BuldozersViewCommand = new RelayCommand(o =>
            {
                CurrentView = BuldozersVM;
            });
            ReportsViewCommand = new RelayCommand(o =>
            {
                CurrentView = ReportsVM;
            });
            CloseAppCommand = new RelayCommand(o =>
            {
                Application.Current.Shutdown();
            });
        }
    }
}