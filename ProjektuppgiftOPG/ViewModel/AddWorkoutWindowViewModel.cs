using ProjektuppgiftOPG.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektuppgiftOPG.ViewModel
{
    public class AddWorkoutWindowViewModel : ViewModelBase
    {
        //Egenskaper
        private DateTime dateInput;
        public DateTime DateInput
        {
            get { return dateInput; }
            set
            {
                dateInput = value;
                OnPropertyChanged();
            }
        }

        private string selectedType;
        public string SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                OnPropertyChanged();
            }
        }

        private string durationInput;
        public string DurationInput
        {
            get { return durationInput; }
            set
            {
                durationInput = value;
                OnPropertyChanged();
            }
        }

        private string caloriesInput;
        public string CaloriesInput
        {
            get { return caloriesInput; }
            set
            {
                caloriesInput = value;
                OnPropertyChanged();
            }
        }

        private string notesInput;
        public string NotesInput
        {
            get { return notesInput; }
            set
            {
                notesInput = value;
                OnPropertyChanged();
            }
        }

    }
}