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
        public int Distance { get; set; }

        //Konstruktor
        public CardioWorkout(DateTime date, string type, TimeSpan duration, int caloriesBurned,
            string notes, int distance) : base(date, type, duration, caloriesBurned, notes)
        {
            Distance = distance;
        }

        //Metod
        public override int CalculateCaloriesBurned()
        {
            // Kaloriberäkning för Cardio
            int caloriesPerKm = 80; //Uppskattat antal kcal/km
            return (int)(Distance * caloriesPerKm);
        }

        // ToString()-metod för att visa information i ListBox
        public override string ToString()
        {
            return $"{Type} - {Date:d}, {Distance} km";
        }
    }
}
