using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DiplomMVVM.Core;
using DiplomMVVM.MVVM.Models;
using DiplomMVVM.MVVM.View;

namespace DiplomMVVM.MVVM.ViewModel
{
    
    public class BuldozersViewModel: ObservableObject
    {
        public RelayCommand AddBuldozerViewCommand { get; set; }
        public RelayCommand DeleteBuldozerViewCommand { get; set; }
        
        public RelayCommand SaveChangesBuldozerViewCommand { get; set; }
        private Бульдозер _newBuldozer = new Бульдозер();
       

        public Бульдозер NewBuldozer
        {
            get => _newBuldozer;
            set
            {
                _newBuldozer = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Бульдозер> _listBuldozers;
        public ObservableCollection<Бульдозер> ListBuldozers 
        { 
            get => _listBuldozers; 
            set
            {
                _listBuldozers = value;
                OnPropertyChanged();
            } 
        }

        private Бульдозер _selectedBuldozer;
        public Бульдозер SelectedBuldozer
        {
            get => _selectedBuldozer;
            set
            {
                _selectedBuldozer = value;
                OnPropertyChanged();
            }
        }
        public BuldozersViewModel()
        {
           
            ListBuldozers = new ObservableCollection<Бульдозер>();
            UpdateView();
           
            #region Commands

            AddBuldozerViewCommand = new RelayCommand(o =>
            {
                try
                {
                    DiplomEntities.GetContext().Бульдозер.Add(NewBuldozer);
                    DiplomEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно добавлены!");
                    NewBuldozer = null;
                    UpdateView();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }

            });
            
            DeleteBuldozerViewCommand = new RelayCommand(o =>
            {
                if (SelectedBuldozer == null) return;
                MessageBoxResult result = MessageBox.Show($"Удалить бульдозер модели {SelectedBuldozer.Модель}?",
                    "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        DiplomEntities.GetContext().Бульдозер.Remove(SelectedBuldozer);
                        ListBuldozers.Remove(SelectedBuldozer);
                        DiplomEntities.GetContext().SaveChanges();
                        OnPropertyChanged();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                }
                
            }, (o) => ListBuldozers.Count > 0);
            
            SaveChangesBuldozerViewCommand = new RelayCommand(o =>
            {
                try
                {
                    DiplomEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно отредактированы!");
                    SelectedBuldozer = null;
                    UpdateView();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            });
            #endregion

        }
        private void UpdateView()
        {
            ListBuldozers.Clear();
            var queryBuldozers = from b in DiplomEntities.GetContext().Бульдозер
                orderby b.Модель
                select b;
            foreach(var b in queryBuldozers) ListBuldozers.Add(b);
        }
        
    }
}