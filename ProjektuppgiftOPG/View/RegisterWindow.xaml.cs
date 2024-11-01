using ProjektuppgiftOPG.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjektuppgiftOPG.Windows
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            DataContext = new RegisterWindowViewModel();

            //Lägg till en händelsehanterare för stängning av fönster
            this.Closing += RegisterWindowClosing;
        }

        private void RegisterWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Om MainWindow fortfarande är öppen, visa det
            if (Application.Current.Windows.OfType<MainWindow>().Any())
            {
                //Gör MainWindow synlig igen
                Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Show();
            }
        }
    }
}
