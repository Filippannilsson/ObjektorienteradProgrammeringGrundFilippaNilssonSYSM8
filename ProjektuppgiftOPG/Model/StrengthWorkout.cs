using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektuppgiftOPG.Model
{
    public class StrengthWorkout : Workout
    {
        //Egenskaper
        public int Repetitions { get; set; }

        //Konstruktor
        public StrengthWorkout(DateTime date, string type, TimeSpan duration, int caloriesBurned,
            string notes, int repetitions) : base(date, type, duration, caloriesBurned, notes)
        {
            Repetitions = repetitions;
        }

        //Metod
        public override int CalculateCaloriesBurned()
        {
            // Kaloriberäkning för Cardio
            return (int)(Duration.TotalMinutes * Repetitions * 0.1);
        }
    }
}
