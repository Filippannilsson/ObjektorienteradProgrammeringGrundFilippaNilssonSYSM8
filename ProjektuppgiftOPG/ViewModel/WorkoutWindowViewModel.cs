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


        // Tillgängliga val för träningspass
        public ObservableCollection<string> AvailableWorkoutTypes { get; set; }
        public ObservableCollection<string> AvailableDurations { get; set; }

        //Egenskaper för filtrering
        private DateTime? selectedDate;
        public DateTime? SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged();
                FilterWorkouts();
            }
        }

        private string selectedWorkoutType;
        public string SelectedWorkoutType
        {
            get { return selectedWorkoutType; }
            set
            {
                selectedWorkoutType = value;
                OnPropertyChanged();
                FilterWorkouts();
            }
        }

        private string selectedDuration;
        public string SelectedDuration
        {
            get { return selectedDuration; }
            set
            {
                selectedDuration = value;
                OnPropertyChanged();
                FilterWorkouts();
            }
        }

        //Kommando för knapparna
        public RelayCommand UserCommand => new RelayCommand(OpenUserWindow);
        public RelayCommand AddWorkoutCommand => new RelayCommand(AddWorkOut);
        public RelayCommand DetailsCommand => new RelayCommand(param => OpenDetails(SelectedWorkout));
        public RelayCommand RemoveCommand => new RelayCommand(RemoveWorkOut);
        public RelayCommand InfoCommand => new RelayCommand(ShowInfo);
        public RelayCommand SignOutCommand => new RelayCommand(SignOut);
        public RelayCommand ClearDateCommand => new RelayCommand(ClearDate);

        //Initierar UserManager och WorkoutManager
        public UserManager UserManager { get; set; }
        public WorkoutManager WorkoutManager { get; set; }

        //Konstruktor
        public WorkoutWindowViewModel(UserManager userManager, ObservableCollection<Workout> workouts, string username, WorkoutManager workoutManager)
        {
            UserManager = userManager;
            Username = username;
            WorkoutManager = workoutManager;

            AvailableWorkoutTypes = new ObservableCollection<string> { "All workouts", "Cardio", "Strength" };

            AvailableDurations = new ObservableCollection<string> { "All durations", "0-30 min", "31-60 min", "> 60 min" };

            //Hämta träningspass till lista
            // Kontrollera om inloggade användaren är admin
            var currentUser = UserManager.GetUsers().FirstOrDefault(u => u.Username == username);
            if (currentUser is AdminUser)
            {
                //Hämta alla träningspass från alla användare
                WorkoutList = new ObservableCollection<Workout>(
                    UserManager.Users.SelectMany(u => u.Workouts));
            }
            else
            {
                //Om inloggad användare inte är admin, använd användarens träningspass
                WorkoutList = workouts;
            }
        }

        //Metod för att öppna UserDetailsWindow
        public void OpenUserWindow(object parameter)
        {
            //Skapa instans av UserDetailsWindow och ViewModel
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow(UserManager, username);
            UserDetailsWindowViewModel userDetailsViewModel = (UserDetailsWindowViewModel)userDetailsWindow.DataContext;

            // Abonnera på UsernameUpdated-händelsen
            userDetailsViewModel.UsernameUpdated += (sender, newUsername) =>
            {
                Username = newUsername; // Uppdatera Username-egenskapen
            };

            //Öppnar UserDetailsWindow
            userDetailsWindow.Show();
        }

        //Metod för att öppna AddWorkoutWindow
        public void AddWorkOut(object parameter)
        {
            // Kontrollera om användaren är admin
            var currentUser = UserManager.GetUsers().FirstOrDefault(u => u.Username == Username);

            if (currentUser is AdminUser adminUser)
            {
                //Meddelande om admin försöker lägga till träningspass
                MessageBox.Show("Admin can not add workouts");
                return;
            }

            // Skapa instans av AddWorkoutWindow och ViewModel
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow(WorkoutManager);
            AddWorkoutWindowViewModel addWorkoutViewModel = (AddWorkoutWindowViewModel)addWorkoutWindow.DataContext;

            // Abonnera på WorkoutSaved-händelsen för att uppdatera WorkoutList
            addWorkoutViewModel.WorkoutSaved += (sender, args) => UpdateWorkoutList();

            //Öppna AddWorkout
            addWorkoutWindow.Show();
        }

        //Metod för att öppna WorkoutDetailsWindow
        public void OpenDetails(Workout workout)
        {
            // Kontrollera om träningspass är markerat
            if (workout == null)
            {
                // Om inget träningspass är markerat, visa varningsmeddelande
                MessageBox.Show("Please select a workout");
                return;
            }

            //Om träningspass är markerat, skapa instans av WorkoutDetailsWindow och ViewModel
            WorkoutDetailsWindow workoutDetailsWindow = new WorkoutDetailsWindow(workout, WorkoutManager);
            WorkoutDetailsWindowViewModel workoutDetailsViewModel = (WorkoutDetailsWindowViewModel)workoutDetailsWindow.DataContext;

            // Abonnera på WorkoutSaved-händelsen för att uppdatera WorkoutList
            workoutDetailsViewModel.WorkoutSaved += (sender, args) => UpdateWorkoutList();

            //Öppna WorkoutDetailsWindow
            workoutDetailsWindow.Show();
        }

        // Metod för att uppdatera WorkoutList
        private void UpdateWorkoutList()
        {
            // Filtrera om workoutList för att få de senaste träningspassen
            FilterWorkouts();
        }

        //Metod för att ta bort ett träningspass
        public void RemoveWorkOut(object parameter)
        {
            // Kontrollera om ett träningspass är valt
            if (SelectedWorkout == null)
            {
                MessageBox.Show("Please select a workout to remove");
                return;
            }

            // Kontrollera om användaren är admin
            var currentUser = UserManager.GetUsers().FirstOrDefault(u => u.Username == Username);

            if (currentUser is AdminUser adminUser)
            {
                // Admin tar bort träningspasset globalt
                adminUser.ManageAllWorkOuts(WorkoutManager, SelectedWorkout);

                // Ta bort träningspasset från användarens lista
                foreach (var user in UserManager.Users)
                {
                    if (user.Workouts.Contains(SelectedWorkout))
                    {
                        user.Workouts.Remove(SelectedWorkout);
                        break; // Bryt loopen när träningspasset är borttaget
                    }
                }

                // Uppdatera listan med alla användares träningspass
                WorkoutList = new ObservableCollection<Workout>(
                    UserManager.Users.SelectMany(u => u.Workouts));
            }
            else
            {
                // För vanliga användare, ta bort deras träningspass
                if (WorkoutManager.Workouts.Contains(SelectedWorkout))
                {
                    WorkoutManager.Workouts.Remove(SelectedWorkout);

                    // Uppdatera listan med endast användarens träningspass
                    WorkoutList = new ObservableCollection<Workout>(WorkoutManager.Workouts);
                }
            }

            SelectedWorkout = null; // Återställ vald träning
        }


        //Metod för att visa info om företaget
        public void ShowInfo(object parameter)
        {
            MessageBox.Show("Fit-Track is an app designed to help you manage " +
                "your workouts. You can:\n\n" +
                "* Add new workouts\n" +
                "* View detailed information about your workouts\n" +
                "* Edit and remove workouts\n" +
                "* Use the filter function to find specific workouts\n");
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

        public void FilterWorkouts()
        {
            //Hämta den inloggade användaren
            var currentUser = UserManager.GetUsers().FirstOrDefault(u => u.Username == Username);

            //Skapa en lista för filtrering
            List<Workout> filteredWorkouts;

            if (currentUser is AdminUser)
            {
                //Om inloggad användare är admin, hämta alla användares träningspass
                filteredWorkouts = UserManager.Users.SelectMany(u => u.Workouts).ToList();
            }
            else
            {
                //Om inloggad användare inte är admin, filtrera endast användarens träningspass
                filteredWorkouts = WorkoutManager.Workouts.ToList();
            }

            //Filtrera efter datum
            if (SelectedDate.HasValue)
            {
                filteredWorkouts = filteredWorkouts.Where(w => w.Date.Date == SelectedDate.Value.Date).ToList();
            }

            // Filtrera efter typ av träningspass, ignorera om "All workouts" är valt
            if (!string.IsNullOrEmpty(SelectedWorkoutType) && SelectedWorkoutType != "All workouts")
            {
                filteredWorkouts = filteredWorkouts.Where(w => w.Type == SelectedWorkoutType).ToList();
            }

            // Filtrera efter varaktighet
            if (SelectedDuration != null)
            {
                switch (SelectedDuration)
                {
                    case "0-30 min":
                        filteredWorkouts = filteredWorkouts.Where(w => w.Duration.TotalMinutes >= 0 && w.Duration.TotalMinutes <= 30).ToList();
                        break;
                    case "31-60 min":
                        filteredWorkouts = filteredWorkouts.Where(w => w.Duration.TotalMinutes > 30 && w.Duration.TotalMinutes <= 60).ToList();
                        break;
                    case "> 60 min":
                        filteredWorkouts = filteredWorkouts.Where(w => w.Duration.TotalMinutes > 60).ToList();
                        break;
                }
            }

            //Uppdatera listan
            WorkoutList = new ObservableCollection<Workout>(filteredWorkouts);
        }

        //Metod för att nollställa datum vid filtrering
        private void ClearDate(object parameter)
        {
            SelectedDate = null;
        }
    }
}
