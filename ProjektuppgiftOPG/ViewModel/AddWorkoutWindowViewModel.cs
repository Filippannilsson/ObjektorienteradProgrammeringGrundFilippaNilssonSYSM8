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

                // Aktivera och återställ inaktiva fält beroende på träningstyp
                if (selectedType == "Cardio")
                {
                    IsDistanceEnabled = true;
                    IsRepetitionsEnabled = false;
                    RepetitionsInput = "0";
                }
                else if (selectedType == "Strength")
                {
                    IsDistanceEnabled = false;
                    IsRepetitionsEnabled = true;
                    DistanceInput = "0";
                }
            }
        }

        private string distanceInput;
        public string DistanceInput
        {
            get { return distanceInput; }
            set
            {
                // Validera att inmatningen är en giltig siffra
                if (!IsValidNumber(value))
                {
                    MessageBox.Show("Distance must be a positive number");
                    return;
                }


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

        private string repetitionsInput;
        public string RepetitionsInput
        {
            get { return repetitionsInput; }
            set
            {
                // Validera att inmatningen är en giltig siffra
                if (!IsValidNumber(value))
                {
                    MessageBox.Show("Repetitions must be a positive number");
                    return;
                }

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
            WorkoutTypes = new ObservableCollection<string> { "Cardio", "Strength" };

            //Sätter dagens datum som standardvärde
            DateInput = DateTime.Now;

        }

        //Metod för att lägga till träningspass
        public void SaveWorkout(object parameter)
        {
            if (!CanSaveWorkout())
            {
                MessageBox.Show("Please fill in all information");
                return;
            }

            // Skapa ett nytt träningspass
            Workout newWorkout = null;

            if (SelectedType == "Cardio")
            {
                // Försök att konvertera DistanceInput till int
                if (int.TryParse(DistanceInput, out int distance))
                {
                    newWorkout = new CardioWorkout(DateInput, SelectedType, DurationInput,
                        CalculatedCalories, NotesInput, distance);
                }
            }
            else if (SelectedType == "Strength")
            {
                // Försök att konvertera RepetitionsInput till int
                if (int.TryParse(RepetitionsInput, out int repetitions))
                {
                    newWorkout = new StrengthWorkout(DateInput, SelectedType, DurationInput,
                        CalculatedCalories, NotesInput, repetitions);
                }
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

        //Metod för att uppdatera antal brända kcal 
        private void UpdateCalculatedCalories()
        {
            Workout newWorkout = null;

            // Skapa en CardioWorkout eller StrengthWorkout baserat på vald typ
            if (SelectedType == "Cardio")
            {
                // Försök göra om DistanceInput till int när mågot matats in
                if (!string.IsNullOrWhiteSpace(DistanceInput) && int.TryParse(DistanceInput, out int distance))
                {
                    newWorkout = new CardioWorkout(DateInput, SelectedType, DurationInput,
                        CalculatedCalories, NotesInput, distance);
                }
            }
            else if (SelectedType == "Strength")
            {
                // Försök göra om RepetitionsInput till int när mågot matats in
                if (!string.IsNullOrWhiteSpace(RepetitionsInput) && int.TryParse(RepetitionsInput, out int repetitions))
                {
                    newWorkout = new StrengthWorkout(DateInput, SelectedType, DurationInput,
                        CalculatedCalories, NotesInput, repetitions);
                }
            }

            if (newWorkout != null)
            {
                // Beräkna kalorier och sätt värdet
                CalculatedCalories = newWorkout.CalculateCaloriesBurned();
            }
        }

        //Metod för att kontrollera om alla fält är ifyllda
        private bool CanSaveWorkout()
        {
            // Kontrollera om fält är ifyllda korrekt
            if (DateInput == default)
                return false;

            if (string.IsNullOrWhiteSpace(SelectedType))
                return false;

            if (DurationInput == TimeSpan.Zero)
                return false;

            if (string.IsNullOrWhiteSpace(NotesInput))
                return false;

            // Om det är ett Cardio-pass, kontrollera att distans är större än 0
            if (SelectedType == "Cardio")
            {
                if (!int.TryParse(DistanceInput, out int distance) || distance <= 0)
                    return false;
            }

            // Om det är ett Strength-pass, kontrollera att repetitions är större än 0
            if (SelectedType == "Strength")
            {
                if (!int.TryParse(RepetitionsInput, out int repetitions) || repetitions <= 0)
                    return false;
            }


            // Om alla fält är giltiga
            return true;
        }

        private bool IsValidNumber(string value)
        {
            // Kontrollerar om strängen är tom eller om den kan konverteras till ett icke-negativt heltal
            return string.IsNullOrWhiteSpace(value) || (int.TryParse(value, out int number) && number >= 0);
        }

    }
}