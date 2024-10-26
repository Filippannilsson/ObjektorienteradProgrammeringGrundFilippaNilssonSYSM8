using ProjektuppgiftOPG.Model;
using ProjektuppgiftOPG.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektuppgiftOPG.ViewModel
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        //Egenskaper
        private Workout workout;
        public Workout Workout
        {
            get { return workout; }
            set
            {
                workout = value;
                OnPropertyChanged();
            }
        }

        private bool isEditable;
        public bool IsEditable
        {
            get { return isEditable; }
            set
            {
                isEditable = value;
                OnPropertyChanged();
            }
        }

        //Knappar för kommando
        public ICommand EditWorkoutCommand => new RelayCommand(EditWorkout);
        public ICommand SaveWorkoutCommand => new RelayCommand(SaveWorkout);

        //Konstruktor
        public WorkoutDetailsWindowViewModel(Workout selectedWorkout)
        {
            //Sätt workout till det valda träningspasset
            Workout = selectedWorkout;
        }

        //Metoder
        public void EditWorkout(object parameter)
        {

        }

        public void SaveWorkout(object parameter)
        {

        }
    }
}
