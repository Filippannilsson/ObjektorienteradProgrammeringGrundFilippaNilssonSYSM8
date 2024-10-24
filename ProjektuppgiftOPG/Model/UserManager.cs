using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektuppgiftOPG.Model
{
    public class UserManager
    {
        //Lista över alla användare
        public static List<User> Users { get; set; }

        //Konstruktor för att initialisera användarlista
        public UserManager()
        {
            Users = new List<User>
            {
                new User("user", "password", "Sweden", "What is your favorite color?", "Red")
                {
                    Workouts = new ObservableCollection<Workout>
                    {
                        new CardioWorkout(DateTime.Now, "Cardio", TimeSpan.FromMinutes(30), 300, "Morning run", 5),
                        new StrengthWorkout(DateTime.Now, "Strength", TimeSpan.FromMinutes(40), 250, "Gym", 1),
                        new CardioWorkout(DateTime.Now, "Cardio", TimeSpan.FromMinutes(70), 500, "Morning run", 11)
                    }
                },
                new AdminUser("admin", "password", "Sweden", "What is your favorite color?", "Red")
            };
        }

        //Skapa metod som returnar listan
        public List<User> GetUsers()
        {
            return Users;
        }
    }

}
