using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        private Perfomance _userPerfomance = new Perfomance();
        public Perfomance UserPerfomance
        {
            get => _userPerfomance;
            set
            {
                _userPerfomance = value;
                OnPropertyChanged();
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
                string FilePath = @"D:/4 курс практика/C# home/DiplomMVVM/TextReports/" + (DateTime.Now.ToString()).Replace(':', ' ') + ".txt"; ;
                using (StreamWriter fileStream = File.CreateText(FilePath))
                {

                    fileStream.WriteLine($"Количество бульдозеров {SelectedPerfomance.CountBuldozers}");
                    fileStream.WriteLine($"Рабочий цикл бульдозера {SelectedPerfomance.Twork} секунд");
                    fileStream.WriteLine($"Производительность бульдозеров {SelectedPerfomance.PerfomanceAllBuldozers} м^3/смена");
                    fileStream.WriteLine($"Необходимое количество бульдозеров {SelectedPerfomance.NeedfullCountBuldozers}");
                }

            });

            CalculateUserModelPerfomance = new RelayCommand(o =>
            {
                UserPerfomance.CalculateTimeWork();
                UserPerfomance.CalculatePerfomance();
                string FilePath = @"D:/4 курс практика/C# home/DiplomMVVM/TextReports/" + (DateTime.Now.ToString()).Replace(':', ' ') + ".txt"; ;
                using (var fileStream = File.CreateText(FilePath))
                {

                    fileStream.WriteLine($"Количество бульдозеров {SelectedPerfomance.CountBuldozers}");
                    fileStream.WriteLine($"Рабочий цикл бульдозера {SelectedPerfomance.Twork} секунд");
                    fileStream.WriteLine($"Производительность бульдозеров {SelectedPerfomance.PerfomanceAllBuldozers} м^3/смена");
                    fileStream.WriteLine($"Необходимое количество бульдозеров {SelectedPerfomance.NeedfullCountBuldozers}");
                }

            });
            #endregion
        }
    }
}