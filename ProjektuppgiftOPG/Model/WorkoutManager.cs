using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektuppgiftOPG.Model
{
    public class WorkoutManager
    {
        //Lista över träningspass
        public ObservableCollection<Workout> Workouts { get; set; }

        //Lista över kopierade träningspass
        public ObservableCollection<Workout> WorkoutTemplates { get; set; } = new ObservableCollection<Workout>();

        //Konstruktor
        public WorkoutManager(ObservableCollection<Workout> workouts)
        {
            Workouts = workouts;
        }

        //Metod för att lägga till träningspass
        public void AddWorkout(Workout workout)
        {
            Workouts.Add(workout);
        }

        //Metod för att kopiera en workout
        public Workout CopyWorkout(Workout workout)
        {
            Workout copiedWorkout = null;

            if (workout is CardioWorkout cardioWorkout)
            {
                copiedWorkout = new CardioWorkout(workout.Date, workout.Type, workout.Duration, workout.CaloriesBurned, workout.Notes, cardioWorkout.Distance);
            }
            else if (workout is StrengthWorkout strengthWorkout)
            {
                copiedWorkout = new StrengthWorkout(workout.Date, workout.Type, workout.Duration, workout.CaloriesBurned, workout.Notes, strengthWorkout.Repetitions);
            }


            //Lägg till kopia som mall i listan
            if (copiedWorkout != null)
            {
                WorkoutTemplates.Add(copiedWorkout);
            }

            return copiedWorkout;
        }
    }
}
