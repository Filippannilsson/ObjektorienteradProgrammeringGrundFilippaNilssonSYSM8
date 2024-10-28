using ProjektuppgiftOPG.Model;
using ProjektuppgiftOPG.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class WorkoutsWindow : Window
    {
        public WorkoutsWindow(UserManager usermanager, ObservableCollection<Workout> workouts, string username, WorkoutManager workoutManager)
        {
            InitializeComponent();
            DataContext = new WorkoutWindowViewModel(usermanager, workouts, username, workoutManager);

            // Lägg till en händelsehanterare för stängning av fönster
            this.Closing += WorkoutsWindowClosing;
        }
        private void WorkoutsWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Om MainWindow fortfarande är öppen, visa det
            if (Application.Current.Windows.OfType<MainWindow>().Any())
            {
                // Gör MainWindow synlig igen
                Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Show();
            }
        }
    }


}
