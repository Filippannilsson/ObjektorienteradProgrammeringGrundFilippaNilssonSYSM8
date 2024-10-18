using ProjektuppgiftOPG.MVVM;
using ProjektuppgiftOPG.Model;
using ProjektuppgiftOPG.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjektuppgiftOPG.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        
        //Egenskaper
        private string username;
        public string Username 
        { 
            get { return username; } 
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        //Lista över registrerade användare
        private List<User> users;

        //Kommando för knapparna
        public RelayCommand SignInCommand => new RelayCommand(SignIn);
        public RelayCommand RegisterCommand => new RelayCommand(Register);
        public RelayCommand ResetCommand => new RelayCommand(Reset);


        //Konstruktor
        public MainWindowViewModel()
        {
            
            //Användare skapad på förhand
            users = new List<User> 
            {
                new User("user", "password", "Sweden", "What is your favorie color?", "Red")
            };

        }

        //Metoder

        public void SignIn(object parameter)
        {

            bool loginSuccessful = false;

            foreach (var user in users)
            {
                //Jämför användarnamn och lösenord med inmatad text
                if (user.Username == Username && user.Password == Password)
                {
                    //Anropar SignIn-metod från User-klassen
                    user.SignIn();
                    loginSuccessful = true;

                    //Öppnar WorkoutsWindow
                    WorkoutsWindow workoutsWindow = new WorkoutsWindow();
                    workoutsWindow.Show();

                    //Stänger ner MainWindow
                    Application.Current.MainWindow.Close();
                }
            }

            if (!loginSuccessful)
            {
                MessageBox.Show("Inloggningen misslyckades, försök igen");
            }
        }

        public void Register(object parameter)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
        }

        public void Reset (object parameter)
        {
            //Glömt lösenord-logik
            MessageBox.Show("Här ska man svara på säkerhetsfråga");
        }
    }
}
