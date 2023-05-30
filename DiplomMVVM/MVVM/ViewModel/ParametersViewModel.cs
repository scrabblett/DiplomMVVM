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
        public ObservableCollection<Грунт> ListGrounds { get; set; }
        private Random rnd = new Random();
        private Грунт _selectedGround;
        public Грунт SelectedGround
        {
            get => _selectedGround;
            set
            {
                _selectedGround = value;
                OnPropertyChanged();
            }
        }
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
            var queryGrounds = from g in DiplomEntities.GetContext().Грунт
                               select g;
            foreach (var g in queryGrounds)
            {
                ListGrounds.Add(g);
            }
        }

        public ParametersViewModel()
        {
            
            ListModels = new ObservableCollection<Бульдозер>();
            ListGrounds = new ObservableCollection<Грунт>();
            GetModels();
            

            #region Commands

            CalculateSelectedModelPerfomance = new RelayCommand(o =>
            {
                try
                {
                    if (SelectedGround == null) throw new Exception("Введите все данные!");
                    switch (SelectedGround.ID)
                    {
                        case 1:
                            SelectedPerfomance.CF_R = 1.05 + rnd.NextDouble() * (1.15 - 1.05);
                            break;
                        case 2:
                            SelectedPerfomance.CF_R = 1.1 + rnd.NextDouble() * (1.25 - 1.1);
                            break;
                        case 3:
                            SelectedPerfomance.CF_R = 1.2 + rnd.NextDouble() * (1.27 - 1.2);
                            break;
                        case 4:
                            SelectedPerfomance.CF_R = 1.2 + rnd.NextDouble() * (1.35 - 1.2);
                            break;
                        case 5:
                            SelectedPerfomance.CF_R = 1.35 + rnd.NextDouble() * (1.5 - 1.35);
                            break;
                        default:
                            SelectedPerfomance.CF_R = 1.05 + rnd.NextDouble() * (1.15 - 1.05);
                            break;
                    }
                    SelectedPerfomance.lengthOtvala = (double)SelectedModel.Длина_отвала;
                    SelectedPerfomance.heightOtvala = (double)SelectedModel.Высота_отвала;
                    SelectedPerfomance.CalculateTimeWork();
                    SelectedPerfomance.CalculatePerfomance();
                    string FilePath = @"TextReports/" + (DateTime.Now.ToString()).Replace(':', ' ') + ".txt"; ;
                    using (StreamWriter fileStream = File.CreateText(FilePath))
                    {

                        fileStream.WriteLine($"Количество бульдозеров {SelectedPerfomance.CountBuldozers}");
                        fileStream.WriteLine($"Рабочий цикл бульдозера {SelectedPerfomance.Twork} секунд");
                        fileStream.WriteLine($"Производительность бульдозеров {SelectedPerfomance.PerfomanceAllBuldozers} м^3/смена");
                        fileStream.WriteLine($"Необходимое количество бульдозеров {SelectedPerfomance.NeedfullCountBuldozers}");
                        fileStream.WriteLine($"Коэффициент рыхления {SelectedPerfomance.CF_R}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message.ToLower()}");
                }
            });

            CalculateUserModelPerfomance = new RelayCommand(o =>
            {
                try
                {
                    if (SelectedGround == null) throw new Exception("Введите все данные!");
                    switch (SelectedGround.ID)
                    {
                        case 1:
                            UserPerfomance.CF_R = 1.05 + rnd.NextDouble() * (1.15 - 1.05);
                            break;
                        case 2:
                            UserPerfomance.CF_R = 1.1 + rnd.NextDouble() * (1.25 - 1.1);
                            break;
                        case 3:
                            UserPerfomance.CF_R = 1.2 + rnd.NextDouble() * (1.27 - 1.2);
                            break;
                        case 4:
                            UserPerfomance.CF_R = 1.2 + rnd.NextDouble() * (1.35 - 1.2);
                            break;
                        case 5:
                            UserPerfomance.CF_R = 1.35 + rnd.NextDouble() * (1.5 - 1.35);
                            break;
                        default:
                            UserPerfomance.CF_R = 1.05 + rnd.NextDouble() * (1.15 - 1.05);
                            break;
                    }
                    UserPerfomance.CalculateTimeWork();
                    UserPerfomance.CalculatePerfomance();
                    string FilePath = @"TextReports/" + (DateTime.Now.ToString()).Replace(':', ' ') + ".txt"; ;
                    using (var fileStream = File.CreateText(FilePath))
                    {

                        fileStream.WriteLine($"Количество бульдозеров {UserPerfomance.CountBuldozers}");
                        fileStream.WriteLine($"Рабочий цикл бульдозера {UserPerfomance.Twork} секунд");
                        fileStream.WriteLine($"Производительность бульдозеров {UserPerfomance.PerfomanceAllBuldozers} м^3/смена");
                        fileStream.WriteLine($"Необходимое количество бульдозеров {UserPerfomance.NeedfullCountBuldozers}");
                        fileStream.WriteLine($"Коэффициент рыхления {UserPerfomance.CF_R}");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message.ToLower()}");
                }
            });
            #endregion
        }
    }
}