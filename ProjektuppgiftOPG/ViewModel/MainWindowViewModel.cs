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
using ProjektuppgiftOPG.View;
using System.Collections.ObjectModel;

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

        //Kommando för knapparna
        public RelayCommand SignInCommand => new RelayCommand(SignIn);
        public RelayCommand RegisterCommand => new RelayCommand(Register);
        public RelayCommand ResetCommand => new RelayCommand(ResetPassword);


        public UserManager UserManager { get; set; }
        public WorkoutManager WorkoutManager { get; set; }

        // Konstruktor
        public MainWindowViewModel()
        {
            UserManager = new UserManager();
            WorkoutManager = new WorkoutManager(new ObservableCollection<Workout>());
        }

        //Metoder
        public void SignIn(object parameter)
        {
            bool loginSuccessful = false;

            //Hämta lista över användare
            List<User> users = UserManager.GetUsers();

            foreach (User user in users)
            {
                //Jämför användarnamn och lösenord med inmatad text
                if (user.Username == Username && user.Password == Password)
                {
                    loginSuccessful = true;

                    //Skapa TwoFactorWindow och skicka UserManager och användaren
                    TwoFactorWindow twoFactorWindow = new TwoFactorWindow(UserManager, user);

                    //Sätter MainWindow som ägare över TwoFactorWindow för att hantera fönster korrekt
                    twoFactorWindow.Owner = Application.Current.MainWindow;

                    //Öppnar TwoFactorWindow som modal dialog, lagra resultatet i dialogResult
                    bool? dialogResult = twoFactorWindow.ShowDialog(); 

                    // Om verifieringen lyckades är dialogResult true
                    if (dialogResult == true)
                    {
                        // Öppnar WorkoutsWindow
                        WorkoutsWindow workoutsWindow = new WorkoutsWindow(UserManager, user.Workouts, Username, new WorkoutManager(user.Workouts));
                        workoutsWindow.Show();

                        // Döljer MainWindow och rensar textboxarna
                        Application.Current.MainWindow.Hide();
                        Username = string.Empty;
                        Password = string.Empty;
                    }
                    break; //Bryt loopen om inloggning lyckas
                }
            }

            if (!loginSuccessful)
            {
                MessageBox.Show("Login failed, try again");
            }
        }

        //Metod för att öppna RegisterWindow
        public void Register(object parameter)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
        }

        //Metod för att öppna ResetPasswordWindow
        public void ResetPassword(object parameter)
        {
            ResetPasswordWindow resetPasswordWindow = new ResetPasswordWindow();
            resetPasswordWindow.Show();
        }
    }
}
