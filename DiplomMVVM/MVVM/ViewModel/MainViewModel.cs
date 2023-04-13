using DiplomMVVM.Core;

namespace DiplomMVVM.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand ParametersViewCommand { get; set; }
        public RelayCommand BuldozersViewCommand { get; set; }
        public ParametersViewModel ParametersVM { get; set; }
        public BuldozersViewModel BuldozersVM { get; set; }
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
            ParametersViewCommand = new RelayCommand(o =>
            {
                CurrentView = ParametersVM;
            });
            BuldozersViewCommand = new RelayCommand(o =>
            {
                CurrentView = BuldozersVM;
            });
        }
    }
}