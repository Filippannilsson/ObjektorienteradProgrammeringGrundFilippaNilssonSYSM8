using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektuppgiftOPG.Model
{
    public class CardioWorkout : Workout
    {
        //Egenskaper
        public double Distance { get; set; }

        //Konstruktor
        public CardioWorkout(DateTime date, string type, TimeSpan duration, int caloriesBurned,
            string notes, double distance) : base(date, type, duration, caloriesBurned, notes)
        {
            Distance = distance;
        }

        //Metod
        public override int CalculateCaloriesBurned()
        {
            // Kaloriberäkning för Cardio
            return (int)(Duration.TotalMinutes * Distance * 0.1);
        }
    }
}
