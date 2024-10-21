using ProjektuppgiftOPG.Model;
using ProjektuppgiftOPG.MVVM;
using ProjektuppgiftOPG.View;
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
    public class ResetPasswordWindowViewModel : ViewModelBase
    {
        // Egenskaper
        private string usernameResetInput;
        public string UsernameResetInput
        {
            get { return usernameResetInput; }
            set
            {
                usernameResetInput = value;
                OnPropertyChanged();
            }
        }

        private string selectedSecurityQuestion;
        public string SelectedSecurityQuestion
        {
            get { return selectedSecurityQuestion; }
            set
            {
                selectedSecurityQuestion = value;
                OnPropertyChanged();
            }
        }

        private string securityAnswerInput;
        public string SecurityAnswerInput
        {
            get { return securityAnswerInput; }
            set
            {
                securityAnswerInput = value;
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

        //Hämta listan av säkerhetsfrågor från RegisterWindowViewModel
        public ObservableCollection<string> SecurityQuestions => RegisterWindowViewModel.Questions;

        //Kommando för att återställa lösenordet
        public RelayCommand ResetCommand => new RelayCommand(ResetPassword);

        //Metod för att återställa lösenord
        private void ResetPassword(object parameter)
        {
            //Skapa variabel för att se om användare finns
            bool userExists = false;

            //Kontrollera om användarnamnet finns
            foreach (User user in MainWindowViewModel.users)
            {
                if (usernameResetInput == user.Username)
                {
                    userExists = true;

                    //Kontrollera om svaret på säkerhetsfrågan är korrekt
                    if (selectedSecurityQuestion == user.SecurityQuestion &&
                        securityAnswerInput == user.SecurityAnswer)
                    {
                        // Kontrollera om det nya lösenordet är giltigt
                        if (newPasswordInput.Length < 8)
                        {
                            MessageBox.Show("The password must be at least 8 characters long");
                            return;
                        }

                        bool hasDigit = false;
                        bool hasSpecialChar = false;

                        // Kontrollera om det finns minst en siffra och ett specialtecken
                        foreach (char c in newPasswordInput)
                        {
                            if (char.IsDigit(c))
                            {
                                hasDigit = true;
                            }
                            else if (!char.IsLetterOrDigit(c))
                            {
                                hasSpecialChar = true;
                            }
                        }

                        if (!hasDigit)
                        {
                            MessageBox.Show("The password must contain at least one digit");
                            return;
                        }
                        if (!hasSpecialChar)
                        {
                            MessageBox.Show("The password must contain at least one special character");
                            return;
                        }

                        //Kontrollera om lösenorden matchar
                        if (newPasswordInput == repeatedPasswordInput)
                        {
                            user.Password = newPasswordInput;
                            MessageBox.Show("Password changed successfully");

                            //Stänger ner ResetPasswordWindow
                            Application.Current.Windows.OfType<ResetPasswordWindow>().FirstOrDefault()?.Close();
                        }
                        else
                        {
                            MessageBox.Show("The passwords do not match, try again");
                        }
                    }
                    else
                    {
                        MessageBox.Show("The answer is incorrect, try again");
                    }
                    break;
                }
            }

            //Meddelande om användaren inte är registrerad
            if (!userExists)
            {
                MessageBox.Show("The user does not exist");
            }
        }
    }
}
