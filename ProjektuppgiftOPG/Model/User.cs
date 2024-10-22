using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektuppgiftOPG.Model
{
    public class User : Person
    {
        //Egenskaper
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public ObservableCollection<Workout> Workouts { get; internal set; }


        //Konstruktor
        public User(string username, string password, string country, string securityQuestion, string securityAnswer) : base(username, password)
        {
            Country = country;
            SecurityQuestion = securityQuestion;
            SecurityAnswer = securityAnswer;

            Workouts = new ObservableCollection<Workout>();
        }

        //Metoder
        public override void SignIn()
        {
                
        }

        public void ResetPassword(string securityAnwser)
        {
            
        }

    }
}
