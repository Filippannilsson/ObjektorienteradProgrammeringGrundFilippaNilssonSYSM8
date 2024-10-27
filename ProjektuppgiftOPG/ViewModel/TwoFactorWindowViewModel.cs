using ProjektuppgiftOPG.Model;
using ProjektuppgiftOPG.MVVM;
using ProjektuppgiftOPG.View;
using ProjektuppgiftOPG.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektuppgiftOPG.ViewModel
{
    public class TwoFactorWindowViewModel : ViewModelBase
    {
        //Egenskaper
        private string verificationCode;
        public string VerificationCode
        {
            get { return verificationCode; }
            set
            {
                verificationCode = value;
                OnPropertyChanged();
            }
        }

        private string generatedCode;

        // Kommando för att begära och bekräfta en kod
        public RelayCommand SendCommand => new RelayCommand(SendVerificationCode);
        public RelayCommand ConfirmCommand => new RelayCommand(ConfirmCode);

        //Konstruktor
        public TwoFactorWindowViewModel(UserManager userManager, User user)
        {

        }

        // Metod för att skicka verifieringskod
        private void SendVerificationCode(object parameter)
        {
            // Generera verifieringskod med 6 siffror och omvandla till string
            Random random = new Random();
            generatedCode = random.Next(100000, 999999).ToString();

            // Visa verifieringskoden i en MessageBox
            MessageBox.Show($"Your verification code is: {generatedCode}", "Verification Code", MessageBoxButton.OK);
        }

        // Metod för att bekräfta koden
        private void ConfirmCode(object parameter)
        {
            if (VerificationCode == generatedCode)
            {
                MessageBox.Show("Verification successful!");

                // Stäng fönstret och returnera true
                var window = Application.Current.Windows.OfType<TwoFactorWindow>().FirstOrDefault(); // Hitta det aktuella fönstret
                if (window != null)
                {
                    window.DialogResult = true; // Sätter DialogResult för att indikera framgång
                    window.Close(); // Stänger TwoFactorWindow
                }
            }
            else
            {
                MessageBox.Show("The verification code is incorrect. Please try again.");
            }
        }
    }
}
