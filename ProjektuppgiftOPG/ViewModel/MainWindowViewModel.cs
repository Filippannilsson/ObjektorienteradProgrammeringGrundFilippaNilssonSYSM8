using ProjektuppgiftOPG.MVVM;
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
    public class MainWindowViewModel
    {
        //Kommando för knapparna
        public ICommand SignInCommand { get; set; }
        public ICommand RegisterCommand { get; set; }


        //Konstruktor
        public MainWindowViewModel()
        {
            SignInCommand = new RelayCommand(SignIn);
            RegisterCommand = new RelayCommand(Register);
        }

        //Metoder

        public void SignIn(object parameter)
        {
            //Inloggningslogik
            MessageBox.Show("Här ska man loggas in");
        }

        public void Register(object parameter)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
        }
    }
}
