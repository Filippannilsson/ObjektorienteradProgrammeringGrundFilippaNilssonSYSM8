using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektuppgiftOPG.Model
{
    public abstract class Person
    {
        //Egenskaper
        public string Username { get; set; }
        public string Password { get; set; }

        //Konstruktor
        public Person(string username, string password)
        {
            Username = username;
            Password = password;
        }

        //Metod
        public abstract void SignIn();
    }
}
