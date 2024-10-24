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
    }
}
