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
        public ObservableCollection<string> Countries { get; set; }

        //Lista för säkerhetsfrågor att välja mellan
        public ObservableCollection<string> Questions { get; set; }

        //Kommando för knapparna
        public RelayCommand RegisterCommand => new RelayCommand(RegisterNewUser);


        //Konstruktor
        public RegisterWindowViewModel(RegisterWindow window)
        {
            registerWindow = window;

            //Skapa länder till comboboxen
            Countries = new ObservableCollection<string> { "Sweden", "Norway", "Denmark", "Finland", "Iceland" };

            //Skapa frågor till comboboxen
            Questions = new ObservableCollection<string> 
            { 
                "What is your favorite color?", 
                "What is the name of your first pet?" 
            };
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

            //FIXA MED SÄKERHETSFRÅGAN SEN

            //Skapar en ny användare
            User newUser = new User(usernameInput, passwordInput, selectedCountry, "What is your favorite color?", "Blue");
            
            //Lägg till ny användare i användarlistan
            MainWindowViewModel.users.Add(newUser);

            MessageBox.Show("Registration successfull");

            //Stänger ner RegisterWindow
            registerWindow.Close();

        }
    }
}
