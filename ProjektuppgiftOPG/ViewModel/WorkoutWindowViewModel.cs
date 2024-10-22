using ProjektuppgiftOPG.Model;
using ProjektuppgiftOPG.MVVM;
using ProjektuppgiftOPG.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

        private Workout selectedWorkout;
        public Workout SelectedWorkout
        {
            get { return selectedWorkout; }
            set
            {
                selectedWorkout = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Workout> workoutList;
        public ObservableCollection<Workout> WorkoutList
        {
            get { return workoutList; }
            set
            {
                workoutList = value;
                OnPropertyChanged();
            }
        }

        //Kommando för knapparna
        public RelayCommand UserCommand => new RelayCommand(OpenUserWindow);
        public RelayCommand AddWorkoutCommand => new RelayCommand(AddWorkOut);
        public RelayCommand DetailsCommand => new RelayCommand(OpenDetails);
        public RelayCommand RemoveCommand => new RelayCommand(RemoveWorkOut);
        public RelayCommand InfoCommand => new RelayCommand(ShowInfo);
        public RelayCommand SignOutCommand => new RelayCommand(SignOut);

        public UserManager UserManager { get; set; }
        public WorkoutManager WorkoutManager { get; set; }
        //Konstruktor
        public WorkoutWindowViewModel(UserManager userManager, ObservableCollection<Workout> workouts)
        {
            UserManager = userManager;
            WorkoutList = workouts;
        }

        //Metod för att öppna UserDetailsWindow
        public void OpenUserWindow(object parameter)
        {
            //Öppnar UserDetailsWindow
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow();
            userDetailsWindow.Show();
        }

        //Metod för att öppna AddWorkoutWindow
        public void AddWorkOut(object parameter)
        {
            //MessageBox.Show("Här öppnas AddWorkoutWindow");
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.Show();
        }

        //Metod för att öppna WorkoutDetailsWindow
        public void OpenDetails(object parameter) //OBS ÄNDRA TILLBAKA TILL (Workout workout), ändrat nu för att det inte går att köra kod annars
        {
            if (SelectedWorkout != null)
            {
                WorkoutDetailsWindow workoutDetailsWindow = new WorkoutDetailsWindow();
                workoutDetailsWindow.Show();
            }
            else
            {
                MessageBox.Show("Select a workout to remove");
            }
        }

        //Metod för att ta bort ett träningspass
        public void RemoveWorkOut(object parameter)
        {
            //Om ett träningspass är valt i listan, ta bort det
            if (SelectedWorkout != null)
            {
                WorkoutManager.Workouts.Remove(SelectedWorkout);
                SelectedWorkout = null;
            }
            else
            {
                MessageBox.Show("Please select a workout to remove");
            }
            
        }

        //Metod för att visa info om företaget
        public void ShowInfo(object parameter)
        {
            MessageBox.Show("Här står lite info om Fit-track");
        }

        //Metod för att logga ut
        public void SignOut(object parameter)
        {
            //Stänger WorkoutWindow och går tillbaka till MainWindow
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            if (mainWindow != null)
            {
                mainWindow.Show(); //Visa MainWindow igen
            }

            //Stänger ner WorkoutsWindow
            Application.Current.Windows.OfType<WorkoutsWindow>().FirstOrDefault()?.Close();
        }
    }
}
