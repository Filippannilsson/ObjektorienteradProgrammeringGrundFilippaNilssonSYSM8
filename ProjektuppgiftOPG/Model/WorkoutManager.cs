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
        // Lista över träningspass
        public ObservableCollection<Workout> Workouts { get; set; }

        // Konstruktor
        public WorkoutManager(ObservableCollection<Workout> workouts)
        {
            Workouts = workouts;
        }

        //Metod för att lägga till träningspass
        public void AddWorkout(Workout workout)
        {
            Workouts.Add(workout);
        }

        // Metod för att kopiera en workout
        public Workout CopyWorkout(Workout originalWorkout)
        {
            if (originalWorkout is CardioWorkout cardioWorkout)
            {
                return new CardioWorkout(
                    cardioWorkout.Date,
                    cardioWorkout.Type,
                    cardioWorkout.Duration,
                    cardioWorkout.CaloriesBurned,
                    cardioWorkout.Notes,
                    cardioWorkout.Distance
                );
            }
            else if (originalWorkout is StrengthWorkout strengthWorkout)
            {
                return new StrengthWorkout(
                    strengthWorkout.Date,
                    strengthWorkout.Type,
                    strengthWorkout.Duration,
                    strengthWorkout.CaloriesBurned,
                    strengthWorkout.Notes,
                    strengthWorkout.Repetitions
                );
            }

            return null; // eller kasta ett undantag om workout-typen är okänd
        }
    }
}
