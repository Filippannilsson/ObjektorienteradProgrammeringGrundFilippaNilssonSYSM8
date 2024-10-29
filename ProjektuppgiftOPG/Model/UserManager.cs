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
                        new CardioWorkout(DateTime.Now, "Cardio", TimeSpan.FromMinutes(30), 400, "Running", 5),
                        new CardioWorkout(new DateTime(2024, 10, 15), "Cardio", TimeSpan.FromMinutes(70), 640, "Swimming", 8),
                        new StrengthWorkout(new DateTime(2024, 10, 23), "Strength", TimeSpan.FromMinutes(40), 200, "Gym", 30),
                        new StrengthWorkout(new DateTime(2024, 10, 26), "Strength", TimeSpan.FromMinutes(80), 400, "Gym", 30)
                    }
                },
                new AdminUser("admin", "password", "Sweden", "What is your favorite color?", "Red")
            };
        }

        //Skapa metod som returnerar listan
        public List<User> GetUsers()
        {
            return Users;
        }
    }

}
