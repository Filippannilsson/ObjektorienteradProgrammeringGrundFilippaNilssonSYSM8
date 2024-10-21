using ProjektuppgiftOPG.Model;
using ProjektuppgiftOPG.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektuppgiftOPG.ViewModel
{
    public class WorkoutWindowViewModel : ViewModelBase
    {
        //Egenskaper
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        private string selectedWorkout;
        public string SelectedWorkout
        {
            get { return selectedWorkout; }
            set
            {
                selectedWorkout = value;
                OnPropertyChanged();
            }
        }

        //Lista över genomförda träningspass
        public ObservableCollection<Workout> WorkoutList { get; set; }

        //Kommando för knapparna
        public RelayCommand SignOutCommand => new RelayCommand(SignOut);
        public RelayCommand UserCommand => new RelayCommand(OpenUserWindow);
        public RelayCommand AddWorkoutCommand => new RelayCommand(AddWorkOut);
        public RelayCommand DetailsCommand => new RelayCommand(OpenDetails);
        public RelayCommand RemoveCommand => new RelayCommand(RemoveWorkOut);
        public RelayCommand InfoCommand => new RelayCommand(ShowInfo);

        //Konstruktor
        public WorkoutWindowViewModel()
        {
            //Skapa träningspass till datagrid
            
        }

        //Metod för att lägga till nytt träningspass
        public void AddWorkOut(object parameter)
        {

        }

        //Metod för att ta bort ett träningspass
        public void RemoveWorkOut(object parameter)
        {

        }

        //Metod för att öppna WorkoutDetailsWindow
        public void OpenDetails(Workout workout)
        {

        }

        //Metod för att logga ut
        public void SignOut(object parameter)
        {

        }

        //Metod för att öppna UserDetailsWindow
        public void OpenUserWindow(object parameter)
        {

        }

        //Metod för att visa info om företaget
        public void ShowInfo(object parameter)
        {

        }



    }
}
