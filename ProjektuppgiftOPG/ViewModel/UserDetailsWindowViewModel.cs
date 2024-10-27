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
    public class UserDetailsWindowViewModel : ViewModelBase
    {
        //Egenskaper

        private string newUsernameInput;
        public string NewUsernameInput
        {
            get { return newUsernameInput; }
            set
            {
                newUsernameInput = value;
                OnPropertyChanged();
            }
        }

        private string newPasswordInput;
        public string NewPasswordInput
        {
            get { return newPasswordInput; }
            set
            {
                newPasswordInput = value;
                OnPropertyChanged();
            }
        }

        private string confirmPasswordInput;
        public string ConfirmPasswordInput
        {
            get { return confirmPasswordInput; }
            set
            {
                confirmPasswordInput = value;
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
        public string SelectedQuestion { get; private set; }


        private string answerInput;
        public string AnswerInput { get; private set; }


        //Lista för länder att välja mellan
        public static ObservableCollection<string> Countries { get; set; }

        //Kommando för knapparna
        public RelayCommand SaveCommand => new RelayCommand(SaveUserDetails);
        public RelayCommand CancelCommand => new RelayCommand(Cancel);



        //Konstruktor
        public UserDetailsWindowViewModel(string selectedQuestion, string answerInput)
        {
            //Skapa länder till comboboxen
            Countries = new ObservableCollection<string> { "Sweden", "Norway", "Denmark", "Finland", "Iceland" };

            SelectedQuestion = selectedQuestion;
            AnswerInput = answerInput;
        }

        //Metoder
        public void SaveUserDetails(object parameter)
        {
            //Kontrollera om användarnamnet är upptaget
            foreach (User user in UserManager.Users) //Hämtar lista från MainWindowViewModel
            {
                if (newUsernameInput == user.Username)
                {
                    MessageBox.Show("The username is already taken, try again");
                    //Avbryt registrering om användarnamn är upptaget
                    return;
                }
            }

            //Kontrollera om lösenordet är giltigt

            if (newPasswordInput.Length < 5)
            {
                MessageBox.Show("The password must be at least 5 characters long");
                return;
            }

            bool hasDigit = false;
            bool hasSpecialchar = false;

            //Kontrollera om det finns minst en siffra och ett specialtecken
            foreach (char c in newPasswordInput)
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
            if (newPasswordInput != confirmPasswordInput)
            {
                MessageBox.Show("The passwords do not match");
                return;
            }

            //Skapar en ny användare om lösenord är korrekt
            User newUser = new User(newUsernameInput, newPasswordInput, selectedCountry, selectedQuestion, answerInput);

            //Lägg till ny användare i användarlistan
            UserManager.Users.Add(newUser);

            MessageBox.Show("Account updated successfully");

            //Stänger ner UserDetailsWindow
            Application.Current.Windows.OfType<UserDetailsWindow>().FirstOrDefault()?.Close();
        }


        public void Cancel(object parameter)
        {

        }
    }
}
    

