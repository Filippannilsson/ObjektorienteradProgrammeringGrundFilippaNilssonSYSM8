using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektuppgiftOPG.Classes
{
    public class AdminUser : User
    {
        //Konstruktor
        public AdminUser (string username, string password, string country, string securityQuestion, 
            string securityAnswer) : base (username, password, country, securityQuestion, securityAnswer)
        {

        }

        //Metod
        public void ManageAllWorkOuts()
        {

        }
    }
}
