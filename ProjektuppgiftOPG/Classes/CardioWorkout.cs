using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektuppgiftOPG.Classes
{
    public class CardioWorkout : Workout
    {
        //Egenskaper
        public int Distance { get; set; }

        //Konstruktor
        public CardioWorkout (DateTime date, string type, TimeSpan duration, int caloriesBurned,
            string notes, int distance) : base (date, type, duration, caloriesBurned, notes)
        {

        }

        //Metod
        public override int CalculateCaloriesBurned()
        {
            return 0;
        }
    }
}
