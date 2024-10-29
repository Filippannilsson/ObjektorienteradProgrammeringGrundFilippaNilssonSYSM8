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

                // Uppdatera bindningar baserat på workout
                OnPropertyChanged(nameof(DateInput));
                OnPropertyChanged(nameof(SelectedType));
                OnPropertyChanged(nameof(DurationInput));
                OnPropertyChanged(nameof(CalculatedCalories));
                OnPropertyChanged(nameof(NotesInput));

                // Kontrollera vilken typ av workout som används
                if (workout is CardioWorkout cardioWorkout)
                {
                    OnPropertyChanged(nameof(DistanceInput));
                    // Rensa repetitions
                    RepetitionsInput = 0;
                    IsDistanceEnabled = true;
                    IsRepetitionsEnabled = false;
                }
                else if (workout is StrengthWorkout strengthWorkout)
                {
                    OnPropertyChanged(nameof(RepetitionsInput));
                    // Rensa distance
                    DistanceInput = 0;
                    IsRepetitionsEnabled = true;
                    IsDistanceEnabled = false;
                }
            }
        }


        public DateTime? DateInput
        {
            get { return Workout.Date; }
            set
            {
                Workout.Date = value ?? DateTime.Now; // Använd nuvarande datum om inget värde sätts
                OnPropertyChanged();
            }
        }


        public string SelectedType
        {
            get { return Workout.Type; }
            set
            {
                Workout.Type = value;
                OnPropertyChanged();

                //Uppdatera aktivering av fält beroende på val
                if (Workout.Type == "Cardio")
                {
                    IsDistanceEnabled = true;
                    IsRepetitionsEnabled = false;
                    // Rensa repetitions
                    RepetitionsInput = 0;
                }
                else if (Workout.Type == "Strength")
                {
                    IsDistanceEnabled = false;
                    IsRepetitionsEnabled = true;
                    // Rensa distance
                    DistanceInput = 0;
                }
            }
        }

        public int DistanceInput
        {
            get
            {
                if (Workout is CardioWorkout cardioWorkout)
                {
                    return cardioWorkout.Distance;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (Workout is CardioWorkout cardioWorkout)
                {
                    cardioWorkout.Distance = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CalculatedCalories));
                }
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
                OnPropertyChanged(nameof(IsDistanceEditable));
            }
        }

        public int RepetitionsInput
        {
            get
            {
                if (Workout is StrengthWorkout strengthWorkout)
                {
                    return strengthWorkout.Repetitions;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (Workout is StrengthWorkout strengthWorkout)
                {
                    strengthWorkout.Repetitions = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CalculatedCalories));
                }
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
                OnPropertyChanged(nameof(IsRepetitionsEditable));
            }
        }

        public int DurationInput
        {
            get { return (int)Workout.Duration.TotalMinutes; }
            set
            {
                Workout.Duration = TimeSpan.FromMinutes(value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(CalculatedCalories));
            }
        }

        public int CalculatedCalories
        {
            get { return Workout.CalculateCaloriesBurned(); }
        }

        public string NotesInput
        {
            get { return Workout.Notes; }
            set
            {
                Workout.Notes = value;
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
                OnPropertyChanged(nameof(IsDistanceEditable));
                OnPropertyChanged(nameof(IsRepetitionsEditable));
            }
        }

        //Avgör om distance eller repetitions ska vara aktivterade beroende på type
        public bool IsDistanceEditable => IsEditable && IsDistanceEnabled;
        public bool IsRepetitionsEditable => IsEditable && IsRepetitionsEnabled;


        //Lista över workout types
        public ObservableCollection<string> WorkoutTypes { get; set; }

        //Eventhandler för att kunna uppdatera WorkoutList när man sparar ändringar

        public event EventHandler WorkoutSaved;

        //Knappar för kommando
        public RelayCommand EditWorkoutCommand => new RelayCommand(EditWorkout);
        public RelayCommand SaveWorkoutCommand => new RelayCommand(SaveWorkout);
        public RelayCommand CopyWorkoutCommand => new RelayCommand(CopyWorkout);


        public WorkoutManager WorkoutManager { get; set; }

        //Konstruktor
        public WorkoutDetailsWindowViewModel(Workout selectedWorkout, WorkoutManager workoutManager)
        {
            //Sätt workout till det valda träningspasset
            Workout = selectedWorkout;

            //Tilldela WorkoutManager 
            WorkoutManager = workoutManager;

            WorkoutTypes = new ObservableCollection<string> { "Cardio", "Strength" };
            SelectedType = Workout.Type;

        }

        //Metoder
        public void EditWorkout(object parameter)
        {
            //Gör fälten redigeringsbara
            IsEditable = true;
        }

        public void SaveWorkout(object parameter)
        {
            // Uppdatera egenskaper
            Workout.Date = DateInput ?? DateTime.Now;
            Workout.Type = SelectedType;
            Workout.Duration = TimeSpan.FromMinutes(DurationInput);
            Workout.Notes = NotesInput;

            if (Workout is CardioWorkout cardioWorkout)
            {
                cardioWorkout.Distance = DistanceInput;
            }
            else if (Workout is StrengthWorkout strengthWorkout)
            {
                strengthWorkout.Repetitions = RepetitionsInput;
            }

            IsEditable = false;

            // Utlös händelsen för att meddela att workout har sparats
            WorkoutSaved?.Invoke(this, EventArgs.Empty);

            //Stänger ner fönstret
            Application.Current.Windows.OfType<WorkoutDetailsWindow>().FirstOrDefault()?.Close();
        }

        public void CopyWorkout(object parameter)
        {
            Workout copiedWorkout = WorkoutManager.CopyWorkout(Workout);

            if (copiedWorkout != null)
            {
                MessageBox.Show("Copy was succesfull! The copied workout is added to templates");

                //Stänger ner fönstret
                Application.Current.Windows.OfType<WorkoutDetailsWindow>().FirstOrDefault()?.Close();
            }
            else
            {
                MessageBox.Show("Failed to copy");
            }
        }
    }
}
