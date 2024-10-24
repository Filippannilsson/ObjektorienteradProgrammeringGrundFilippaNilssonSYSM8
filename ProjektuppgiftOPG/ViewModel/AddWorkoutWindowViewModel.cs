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

namespace ProjektuppgiftOPG.ViewModel
{
    public class AddWorkoutWindowViewModel : ViewModelBase
    {
        //Egenskaper
        private DateTime dateInput;
        public DateTime DateInput
        {
            get { return dateInput; }
            set
            {
                dateInput = value;
                OnPropertyChanged();
            }
        }

        private string selectedType;
        public string SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                OnPropertyChanged();
                UpdateCalculatedCalories();

                // Aktivera distance om Cardio är vald, annars inaktivera
                IsDistanceEnabled = selectedType == "Cardio";

                // Aktivera repetitions om Strength är vald, annars inaktivera
                IsRepetitionsEnabled = selectedType == "Strength";
            }
        }

        private double distanceInput;
        public double DistanceInput
        {
            get { return distanceInput; }
            set
            {
                distanceInput = value;
                OnPropertyChanged();

                UpdateCalculatedCalories();
            }
        }

        private bool isDistanceEnabled;
        public bool IsDistanceEnabled
        {
            get { return isDistanceEnabled; }
            set
            {
                isDistanceEnabled = value;
                OnPropertyChanged();
            }
        }

        private int repetitionsInput;
        public int RepetitionsInput
        {
            get { return repetitionsInput; }
            set
            {
                repetitionsInput = value;
                OnPropertyChanged();

                UpdateCalculatedCalories();
            }
        }

        private bool isRepetitionsEnabled;
        public bool IsRepetitionsEnabled
        {
            get { return isRepetitionsEnabled; }
            set
            {
                isRepetitionsEnabled = value;
                OnPropertyChanged();
            }
        }


        private TimeSpan durationInput;
        public TimeSpan DurationInput
        {
            get { return durationInput; }
            set
            {
                durationInput = value;
                OnPropertyChanged();

                UpdateCalculatedCalories();
            }
        }

        private string durationInputString;
        public string DurationInputString
        {
            get { return durationInputString; }
            set
            {
                durationInputString = value;
                OnPropertyChanged();

                // Konvertera strängen till TimeSpan
                SetDurationFromString(value); 
            }
        }

        private int calculatedCalories;
        public int CalculatedCalories
        {
            get { return calculatedCalories; }
            set
            {
                calculatedCalories = value;
                OnPropertyChanged();
            }
        }

        private string notesInput;
        public string NotesInput
        {
            get { return notesInput; }
            set
            {
                notesInput = value;
                OnPropertyChanged();
            }
        }

        //Lista för workout types att välja mellan
        public static ObservableCollection<string> WorkoutTypes { get; set; }

        //Kommando för knapp
        public RelayCommand AddWorkoutCommand => new RelayCommand(SaveWorkout);


        public WorkoutManager WorkoutManager { get; set; }
        //Konstruktor
        public AddWorkoutWindowViewModel(WorkoutManager workoutManager)
        {

            WorkoutManager = workoutManager;

            //Skapa alternativ till comboboxen
            WorkoutTypes = new ObservableCollection<string> { "Cardio", "Strength", "Other" };

            //Sätter dagens datum som standardvärde
            DateInput = DateTime.Now;

        }

        //Metod för att lägga till träningspass
        public void SaveWorkout(object parameter)
        {
            // Skapa ett nytt träningspass
            Workout newWorkout = null;

            if (SelectedType == "Cardio")
            {
                newWorkout = new CardioWorkout(DateInput, SelectedType, DurationInput,
                    CalculatedCalories, NotesInput, DistanceInput);
            }
            else if (SelectedType == "Strength")
            {
                newWorkout = new StrengthWorkout(DateInput, SelectedType, DurationInput,
                    CalculatedCalories, NotesInput, RepetitionsInput);
            }

            // Lägg till träningspasset i WorkoutManager
            WorkoutManager.AddWorkout(newWorkout);

            // Stäng fönstret efter att ha lagt till
            Application.Current.Windows.OfType<AddWorkoutWindow>().FirstOrDefault()?.Close();
        }

        // Hjälpmetod för att konvertera sträng till TimeSpan
        private void SetDurationFromString(string durationString)
        {
            if (int.TryParse(durationString, out int minutes))
            {
                DurationInput = TimeSpan.FromMinutes(minutes);
            }
            else
            {
                //Vid ogiltig inmatning, sätt till 0
                DurationInput = TimeSpan.Zero;
            }
        }

        private void UpdateCalculatedCalories()
        {
            Workout newWorkout = null;

            // Skapa ett CardioWorkout eller StrengthWorkout baserat på vald typ
            if (SelectedType == "Cardio")
            {
                newWorkout = new CardioWorkout(DateInput, SelectedType, DurationInput, 0, NotesInput, DistanceInput);
            }
            else if (SelectedType == "Strength")
            {
                newWorkout = new StrengthWorkout(DateInput, SelectedType, DurationInput, 0, NotesInput, RepetitionsInput);
            }

            if (newWorkout != null)
            {
                // Beräkna kalorier och sätt värdet
                CalculatedCalories = newWorkout.CalculateCaloriesBurned();
            }
        }

    }
}