using Microsoft.Win32;
using ProjektuppgiftOPG.Model;
using ProjektuppgiftOPG.MVVM;
using ProjektuppgiftOPG.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektuppgiftOPG.ViewModel
{
    public class RegisterWindowViewModel : ViewModelBase
    {
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

        private string repeatedPasswordInput;
        public string RepeatedPasswordInput
        {
            get { return repeatedPasswordInput; }
            set
            {
                repeatedPasswordInput = value;
                OnPropertyChanged();
            }
        }

        private string selectedCountry;
        public string SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                selectedCountry = value;
                OnPropertyChanged();
            }
        }

        private string selectedQuestion;
        public string SelectedQuestion
        {
            get { return selectedQuestion; }
            set
            {
                selectedQuestion = value;
                OnPropertyChanged();
            }
        }

        private string answerInput;
        public string AnswerInput
        {
            get { return answerInput; }
            set
            {
                answerInput = value;
                OnPropertyChanged();
            }
        }

        //Lista för länder att välja mellan
        public static ObservableCollection<string> Countries { get; set; }

        //Lista för säkerhetsfrågor att välja mellan
        public static ObservableCollection<string> Questions { get; } = new ObservableCollection<string>
        {
            "What is your favorite color?",
            "What is the name of your first pet?"
        };

        //Kommando för knapparna
        public RelayCommand RegisterCommand => new RelayCommand(RegisterNewUser);


        //Konstruktor
        public RegisterWindowViewModel()
        {
            //Skapa länder till comboboxen
            Countries = new ObservableCollection<string> { "Sweden", "Norway", "Denmark", "Finland", "Iceland" };

        }

        //Metod för att registera ny användare
        public void RegisterNewUser(object parameter)
        {
            //Kontrollera om användarnamnet är upptaget
            foreach (User user in MainWindowViewModel.users) //Hämtar lista från MainWindowViewModel
            {
                if (usernameInput == user.Username)
                {
                    MessageBox.Show("The username is already taken, try again");
                    //Avbryt registrering om användarnamn är upptaget
                    return;
                }
            }

            //Kontrollera om lösenordet är giltigt

            if (passwordInput.Length < 8)
            {
                MessageBox.Show("The password must be at least 8 characters long");
                return;
            }

            bool hasDigit = false;
            bool hasSpecialchar = false;

            //Kontrollera om det finns minst en siffra och ett specialtecken
            foreach (char c in passwordInput)
            {
                if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
                else if (!char.IsLetterOrDigit(c))
                {
                    hasSpecialchar = true;
                }
            }

            if (!hasDigit)
            {
                MessageBox.Show("The password must contain at least one digit");
                return;
            }
            if (!hasSpecialchar)
            {
                MessageBox.Show("The password must contain at least one special character");
                return;
            }

            //Kontrollera om lösenorden matchar
            if (passwordInput != repeatedPasswordInput)
            {
                MessageBox.Show("The passwords do not match");
                return;
            }

            //Skapar en ny användare om lösenord är korrekt
            User newUser = new User(usernameInput, passwordInput, selectedCountry, selectedQuestion, answerInput);

            //Lägg till ny användare i användarlistan
            MainWindowViewModel.users.Add(newUser);

            MessageBox.Show("Registration successfull");

            //Stänger ner RegisterWindow
            Application.Current.Windows.OfType<RegisterWindow>().FirstOrDefault()?.Close();
        }
    }
}
