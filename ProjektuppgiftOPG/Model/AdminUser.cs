using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektuppgiftOPG.Model
{
    public class AdminUser : User
    {
        //Konstruktor
        public AdminUser(string username, string password, string country, string securityQuestion,
            string securityAnswer) : base(username, password, country, securityQuestion, securityAnswer)
        {

        }

        //Metod
        public void ManageAllWorkOuts(WorkoutManager workoutManager, Workout workoutToRemove = null)
        {
            //Hämta alla träningspass
            ObservableCollection<Workout> allWorkouts = workoutManager.Workouts;

            //Om träningspass ska tas bort
            if (workoutToRemove != null && allWorkouts.Contains(workoutToRemove))
            {
                //Ta bort valt träningspass
                allWorkouts.Remove(workoutToRemove);
            }
        }
    }
}
