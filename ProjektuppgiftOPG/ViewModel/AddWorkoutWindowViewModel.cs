﻿using ProjektuppgiftOPG.Model;
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

        private string durationInput;
        public string DurationInput
        {
            get { return durationInput; }
            set
            {
                durationInput = value;
                OnPropertyChanged();
                UpdateCalculatedCalories();
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

        private Workout selectedTemplate;
        public Workout SelectedTemplate
        {
            get { return selectedTemplate; }
            set
            {
                selectedTemplate = value;
                OnPropertyChanged();
                if (selectedTemplate != null)
                {
                    LoadTemplate(selectedTemplate);
                }
            }
        }

        //Lista för workout types att välja mellan
        public static ObservableCollection<string> WorkoutTypes { get; set; }

        // Lista över kopierade träningspass som mallar
        public ObservableCollection<Workout> WorkoutTemplates { get; set; }

        //Eventhandler för att kunna uppdatera WorkoutList när man sparar ändringar

        public event EventHandler WorkoutSaved;

        //Kommando för knapp
        public RelayCommand AddWorkoutCommand => new RelayCommand(SaveWorkout);


        public WorkoutManager WorkoutManager { get; set; }
        //Konstruktor
        public AddWorkoutWindowViewModel(WorkoutManager workoutManager)
        {
            WorkoutManager = workoutManager;

            //Skapa alternativ till comboboxen
            WorkoutTypes = new ObservableCollection<string> { "Cardio", "Strength" };

            //Tilldela listan av kopierade mallar från WorkoutManager
            WorkoutTemplates = WorkoutManager.WorkoutTemplates;

            //Sätter dagens datum som standardvärde
            DateInput = DateTime.Now;
        }


        //Metod för att lägga till träningspass
        public void SaveWorkout(object parameter)
        {
            //Kontrollera om alla fält är ifyllda
            if (!CanSaveWorkout())
            {
                MessageBox.Show("Please fill in all information");
                return;
            }

            // Skapa ett nytt träningspass
            Workout newWorkout = null;

            if (SelectedType == "Cardio")
            {
                // Försök att konvertera DistanceInput och DurationInput till int
                if (int.TryParse(DistanceInput, out int distance) && int.TryParse(DurationInput, out int duration))
                {
                    newWorkout = new CardioWorkout(DateInput, SelectedType, TimeSpan.FromMinutes(duration),
                        CalculatedCalories, NotesInput, distance);
                }
            }
            else if (SelectedType == "Strength")
            {
                // Försök att konvertera RepetitionsInput och DurationInput till int
                if (int.TryParse(RepetitionsInput, out int repetitions) && int.TryParse(DurationInput, out int duration))
                {
                    newWorkout = new StrengthWorkout(DateInput, SelectedType, TimeSpan.FromMinutes(duration),
                        CalculatedCalories, NotesInput, repetitions);
                }
            }
            // Lägg till träningspasset i WorkoutManager
            WorkoutManager.AddWorkout(newWorkout);

            // Utlös händelsen i WorkoutsWindow för att meddela att workout har sparats
            WorkoutSaved?.Invoke(this, EventArgs.Empty);

            // Stäng fönstret efter att ha lagt till
            Application.Current.Windows.OfType<AddWorkoutWindow>().FirstOrDefault()?.Close();
        }

        //Metod för att uppdatera antal brända kcal 
        private void UpdateCalculatedCalories()
        {
            Workout newWorkout = null;

            // Skapa en CardioWorkout eller StrengthWorkout baserat på vald typ
            if (SelectedType == "Cardio")
            {
                // Försök göra om DistanceInput och DurationInput till int när mågot matats in
                if (!string.IsNullOrWhiteSpace(DistanceInput) && int.TryParse(DistanceInput, out int distance)
                    && (!string.IsNullOrWhiteSpace(DurationInput) && int.TryParse(DurationInput, out int duration)))
                {
                    newWorkout = new CardioWorkout(DateInput, SelectedType, TimeSpan.FromMinutes(duration),
                        CalculatedCalories, NotesInput, distance);
                }
            }
            else if (SelectedType == "Strength")
            {
                // Försök göra om RepetitionsInput och DurationInput till int när mågot matats in
                if (!string.IsNullOrWhiteSpace(RepetitionsInput) && int.TryParse(RepetitionsInput, out int repetitions)
                    && (!string.IsNullOrWhiteSpace(DurationInput) && int.TryParse(DurationInput, out int duration)))
                {
                    newWorkout = new StrengthWorkout(DateInput, SelectedType, TimeSpan.FromMinutes(duration),
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

            if (string.IsNullOrWhiteSpace(DurationInput))
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

        // Metod för att fylla i formulärfält från en vald template
        private void LoadTemplate(Workout template)
        {
            DateInput = template.Date;
            SelectedType = template.Type;
            DurationInput = template.Duration.TotalMinutes.ToString();
            NotesInput = template.Notes;

            if (template is CardioWorkout cardio)
            {
                DistanceInput = cardio.Distance.ToString();
                RepetitionsInput = "0";
            }
            else if (template is StrengthWorkout strength)
            {
                RepetitionsInput = strength.Repetitions.ToString();
                DistanceInput = "0";
            }
        }
    }
}
