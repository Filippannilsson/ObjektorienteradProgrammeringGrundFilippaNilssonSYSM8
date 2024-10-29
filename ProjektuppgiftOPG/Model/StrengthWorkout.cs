using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            //Kaloriberäkning för styrketräning
            double caloriesPerMinute = 5; //Uppskattat antal kalorier brända per minut
            return (int)(caloriesPerMinute * Duration.TotalMinutes);
        }

        //Metod för att visa information i Template-ListBox
        public override string ToString()
        {
            return $"{Type} - {Date:d}, {Repetitions} km";
        }
    }
}
