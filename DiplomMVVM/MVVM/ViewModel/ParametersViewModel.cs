using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DiplomMVVM.Core;
using DiplomMVVM.MVVM.Models;

namespace DiplomMVVM.MVVM.ViewModel
{
    public class ParametersViewModel: ObservableObject
    {
        public RelayCommand CalculateSelectedModelPerfomance { get; set; }
        public RelayCommand CalculateUserModelPerfomance { get; set; }
        public ObservableCollection<Бульдозер> ListModels { get; set; }

        private Бульдозер _selectedModel = new Бульдозер();
        public Бульдозер SelectedModel
        {
            get => _selectedModel;
            set
            {
                _selectedModel = value;
                OnPropertyChanged();
            }
        }
        private Бульдозер _userModel = new Бульдозер();
        public Бульдозер UserModel
        {
            get => _userModel;
            set
            {
                _userModel = value;
                OnPropertyChanged();
            }
        }
        private Perfomance _selectedPerfomance = new Perfomance();
        public Perfomance SelectedPerfomance
        {
            get => _selectedPerfomance;
            set
            {
                _selectedPerfomance = value;
                OnPropertyChanged();
            }
        }
        private Perfomance _userPerfomance { get; set; }
        public Perfomance UserPerfomance
        {
            get => _userPerfomance;
            set
            {
                _userPerfomance = value;
            }
        }

        private void GetModels()
        {
            var queryBuldozers = from b in DiplomEntities.GetContext().Бульдозер
                orderby b.Модель
                select b;
            foreach (Бульдозер b in queryBuldozers)
            {
                ListModels.Add(b);
            }
        }

        public ParametersViewModel()
        {
            
            ListModels = new ObservableCollection<Бульдозер>();
            GetModels();

            #region Commands

            CalculateSelectedModelPerfomance = new RelayCommand(o =>
            {
                SelectedPerfomance.lengthOtvala = (double)SelectedModel.Длина_отвала;
                SelectedPerfomance.heightOtvala = (double)SelectedModel.Высота_отвала;
                SelectedPerfomance.CalculateTimeWork();
                SelectedPerfomance.CalculatePerfomance();
                
            });

            #endregion
        }
    }
}