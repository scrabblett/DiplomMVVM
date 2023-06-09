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
                    Отчет report = new Отчет();
                    report.ID_бульдозера = SelectedModel.ID;
                    report.ID_грунта = SelectedGround.ID;
                    report.Необходимое_количество_бульдозеров = SelectedPerfomance.NeedfullCountBuldozers;
                    report.Рабочий_цикл_бульдозера = SelectedPerfomance.Twork;
                    report.Объем_призмы_волочения = SelectedPerfomance.Vprizm;
                    report.Длина_отвала_бульдозера = SelectedModel.Длина_отвала;
                    report.Высота_отвала_бульдозера = SelectedModel.Высота_отвала;
                    report.Производительность_бульдозера = SelectedPerfomance.PerfomanceBuldozer;
                    report.Количество_автосамосвалов = SelectedPerfomance.CountTrucks;
                    report.Длина_передвижения_грунта = SelectedPerfomance.LengthPeredvGrunta;
                    DiplomEntities.GetContext().Отчет.Add(report);
                    DiplomEntities.GetContext().SaveChanges();
                    string FilePath = @"TextReports/" + (DateTime.Now.ToString()).Replace(':', ' ') + ".txt"; ;
                    using (StreamWriter fileStream = File.CreateText(FilePath))
                    {
                        fileStream.WriteLine($"Количество бульдозеров {SelectedPerfomance.CountBuldozers} шт");
                        fileStream.WriteLine($"Рабочий цикл бульдозера {SelectedPerfomance.Twork} секунд");
                        fileStream.WriteLine($"Производительность бульдозеров {SelectedPerfomance.PerfomanceAllBuldozers} м^3/смена");
                        fileStream.WriteLine($"Объем призмы волочения бульдозера {SelectedPerfomance.Vprizm} м^3");
                        fileStream.WriteLine($"Длина отвала бульдозера {SelectedPerfomance.lengthOtvala} м");
                        fileStream.WriteLine($"Высота отвала бульдозера {SelectedPerfomance.heightOtvala} м");
                        fileStream.WriteLine($"Длина передвижения грунта {SelectedPerfomance.LengthPeredvGrunta} м");
                        fileStream.WriteLine($"Количество автосамосвалов в смену {SelectedPerfomance.CountTrucks} шт");
                        fileStream.WriteLine($"Необходимое количество бульдозеров {SelectedPerfomance.NeedfullCountBuldozers}");
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
                    Отчет report = new Отчет();
                    report.ID_грунта = SelectedGround.ID;
                    report.Необходимое_количество_бульдозеров = UserPerfomance.NeedfullCountBuldozers;
                    report.Рабочий_цикл_бульдозера = UserPerfomance.Twork;
                    report.Объем_призмы_волочения = UserPerfomance.Vprizm;
                    report.Длина_отвала_бульдозера = UserPerfomance.lengthOtvala;
                    report.Высота_отвала_бульдозера = UserPerfomance.heightOtvala;
                    report.Производительность_бульдозера = UserPerfomance.PerfomanceBuldozer;
                    report.Количество_автосамосвалов = UserPerfomance.CountTrucks;
                    report.Длина_передвижения_грунта = UserPerfomance.LengthPeredvGrunta;
                    DiplomEntities.GetContext().Отчет.Add(report);
                    DiplomEntities.GetContext().SaveChanges();
                    string FilePath = @"TextReports/" + (DateTime.Now.ToString()).Replace(':', ' ') + ".txt"; ;
                    using (var fileStream = File.CreateText(FilePath))
                    {

                        fileStream.WriteLine($"Количество бульдозеров {UserPerfomance.CountBuldozers}");
                        fileStream.WriteLine($"Рабочий цикл бульдозера {UserPerfomance.Twork} секунд");
                        fileStream.WriteLine($"Производительность бульдозеров {UserPerfomance.PerfomanceAllBuldozers} м^3/смена");
                        fileStream.WriteLine($"Объем призмы волочения бульдозера {UserPerfomance.Vprizm} м^3");
                        fileStream.WriteLine($"Длина отвала бульдозера {UserPerfomance.lengthOtvala} м");
                        fileStream.WriteLine($"Высота отвала бульдозера {UserPerfomance.heightOtvala} м");
                        fileStream.WriteLine($"Длина передвижения грунта {UserPerfomance.LengthPeredvGrunta} м");
                        fileStream.WriteLine($"Количество автосамосвалов в смену {UserPerfomance.CountTrucks} шт");
                        fileStream.WriteLine($"Необходимое количество бульдозеров {UserPerfomance.NeedfullCountBuldozers}");


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