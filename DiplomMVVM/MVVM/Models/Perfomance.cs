using DiplomMVVM.Core;
using System;

namespace DiplomMVVM.MVVM.Models
{
    public class Perfomance: ObservableObject
    {
        #region Trucks

        private int? _countTrucks = null;
        public int? CountTrucks
        {
            get => _countTrucks;
            set
            {
                _countTrucks = value;
            }
        }
        private double _truckPerfomance = 106.18;
        public double TruckPerfomance
        {
            get => _truckPerfomance;
        }

        #endregion

        #region Buldozers

        private int? _countBuldozers = null;
        public int? CountBuldozers
        {
            get => _countBuldozers;
            set
            {
                _countBuldozers = value;
                OnPropertyChanged();
            }
        }

        private int? _needfullCountBuldozers = null;
        
        public int? NeedfullCountBuldozers
        {
            get => _needfullCountBuldozers;
            set
            {
                _needfullCountBuldozers = value;
                OnPropertyChanged();
            }
        }

        private Random rnd = new Random();
        private double? _timeWork = null;
        private double _t1;
        private double _t2;
        private double _t3;
        public double SECONDARY_OPERATIONS { get; set; }
        public const double FIRST_SPEED = 0.555556;
        public const double SECOND_SPEED = 1.11111;
        public const double THIRD_SPEED = 1.66667;
        public double CF_BUKS { get; set; }
        public double lengthRezaniya { get; set; }
        public double Vprizm { get; set; }
        public double lengthOtvala { get; set; }
        public double heightOtvala { get; set; }
        public const double CF_POPR = 0.8;
        public double CF_USE { get; set; }
        public double CF_H { get; set; }
        public double CF_R { get; set; }
        public double CF_UKL { get; set; }
        public double vPeredvGrunta { get; set; }
        private double? _lengthPeredvGrunta = null;
        public double? LengthPeredvGrunta 
        { 
            get => _lengthPeredvGrunta; 
            set 
            { 
                _lengthPeredvGrunta = value;

            } 
        }
        
        public double? Twork 
        { 
            get => _timeWork;
            private set
            {
                _timeWork = value;
                OnPropertyChanged();
            } 
                
        }
        public double T1 { get => _t1;
            private set => _t1 = value;
        }
        public double T2 { get => _t2;
            private set => _t2 = value;
        }
        public double T3 { get => _t3;
            private set => _t3 = value;
        }

        private double? _perfomanceBuldozer;
        public double? PerfomanceBuldozer { get => _perfomanceBuldozer;
            private set => _perfomanceBuldozer = value;
        }

        private double? _perfomanceAllBuldozers = null;

        public double? PerfomanceAllBuldozers 
        { 
            get => _perfomanceAllBuldozers; 
            set 
            { 
                _perfomanceAllBuldozers = value;
                OnPropertyChanged();
            } 
        }
        

        public void CalculateTimeWork()
        {
            SECONDARY_OPERATIONS = rnd.Next(16, 18);
            CF_BUKS = 0.18 + rnd.NextDouble() * (0.22 - 0.18);
            CF_H = 0.08 + rnd.NextDouble() * (0.25 - 0.08);
            CF_R = 1.15 + rnd.NextDouble() * (1.3 - 1.15);
            Vprizm = ((lengthOtvala * (Math.Pow(heightOtvala, 2)) / 2) * CF_POPR);
            lengthRezaniya = Vprizm / ((lengthOtvala * CF_H) * CF_R);
            T1 = (lengthRezaniya) / (FIRST_SPEED * (1 - CF_BUKS));
            vPeredvGrunta = SECOND_SPEED * (1 - CF_BUKS);
            T2 = (double)((LengthPeredvGrunta - lengthRezaniya) / (vPeredvGrunta));
            T3 = (double)(LengthPeredvGrunta / THIRD_SPEED);
            Twork = Math.Round((T1 + T2 + T3 + SECONDARY_OPERATIONS),2);

        }

        public void CalculatePerfomance()
        {
            CF_USE = 0.8 + rnd.NextDouble() * (0.9 - 0.8);
            CF_UKL = 0.67 + rnd.NextDouble() * (1 - 0.67);
            PerfomanceBuldozer = Math.Round((double)(3600 * Vprizm * CF_USE * CF_UKL / (Twork * CF_R)), 3);
            PerfomanceAllBuldozers = (double)(PerfomanceBuldozer * CountBuldozers);
            NeedfullCountBuldozers = (int?)Math.Ceiling((double)(TruckPerfomance * _countTrucks / PerfomanceBuldozer));
        }

        #endregion
        
    }
}