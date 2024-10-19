using Microsoft.Win32;
using ProjektuppgiftOPG.Model;
using ProjektuppgiftOPG.MVVM;
using ProjektuppgiftOPG.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektuppgiftOPG.ViewModel
{
    public class RegisterWindowViewModel : ViewModelBase
    {
        //Variabel för RegisterWindow
        private RegisterWindow registerWindow;

        //Egenskaper
        private string usernameInput;
        public string UsernameInput
        {
            get { return usernameInput; }
            set
            {
                usernameInput = value;
                OnPropertyChanged();
            }
        }

        private string passwordInput;
        public string PasswordInput
        {
            get { return passwordInput; }
            set
            {
                passwordInput = value;
                OnPropertyChanged();
            }
        }

        private string countryComboBox;
        public string CountryComboBox
        {
            get { return countryComboBox; }
            set
            {
                countryComboBox = value;
                OnPropertyChanged();
            }
        }

        //Lista över registrerade användare
        private List<User> users;

        //Kommando för knapparna
        public RelayCommand RegisterCommand => new RelayCommand(RegisterNewUser);


        //Konstruktor
        public RegisterWindowViewModel(RegisterWindow window)
        {
            registerWindow = window;

            //Användare skapad på förhand
            users = new List<User>
            {
                new User("user", "password", "Sweden", "What is your favorie color?", "Red")
            };

        }

        //Metod för att registera ny användare
        public void RegisterNewUser(object parameter)
        {
            foreach (User user in users)
            {
                if (usernameInput == user.Username)
                {
                    MessageBox.Show("The username is already taken, try again");
                }
            }

            //FIXA MED SÄKERHETSFRÅGAN SEN

            //Skapar en ny användare
            User newUser = new User(usernameInput, passwordInput, countryComboBox, "What is your favorite color?", "Blue");
            
            //Lägg till ny användare i användarlistan
            users.Add(newUser);

            MessageBox.Show("Registration successfull");

            //Stänger ner RegisterWindow
            registerWindow.Close();

        }
    }
}
