using ProjektuppgiftOPG.Model;
using ProjektuppgiftOPG.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class AddWorkoutWindow : Window
    {
        public AddWorkoutWindow(WorkoutManager workoutManager)
        {
            InitializeComponent();
            DataContext = new AddWorkoutWindowViewModel(workoutManager);
        }

        // Metod som begränsar inmatningen till endast siffror
        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Regular expression som tillåter endast siffror
            Regex regex = new Regex("^[0-9]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
