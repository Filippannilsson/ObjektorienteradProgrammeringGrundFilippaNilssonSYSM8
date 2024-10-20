﻿using ProjektuppgiftOPG.Model;
using ProjektuppgiftOPG.MVVM;
using ProjektuppgiftOPG.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjektuppgiftOPG.ViewModel
{
    public class WorkoutWindowViewModel : ViewModelBase
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

        private Workout selectedWorkout;
        public Workout SelectedWorkout
        {
            get { return selectedWorkout; }
            set
            {
                selectedWorkout = value;
                OnPropertyChanged();
            }
        }

        //Lista över genomförda träningspass
        public ObservableCollection<Workout> WorkoutList { get; set; }

        //Kommando för knapparna
        public RelayCommand SignOutCommand => new RelayCommand(SignOut);
        public RelayCommand UserCommand => new RelayCommand(OpenUserWindow);
        public RelayCommand AddWorkoutCommand => new RelayCommand(AddWorkOut);
        public RelayCommand DetailsCommand => new RelayCommand(OpenDetails);
        public RelayCommand RemoveCommand => new RelayCommand(RemoveWorkOut);
        public RelayCommand InfoCommand => new RelayCommand(ShowInfo);

        //Konstruktor
        public WorkoutWindowViewModel()
        {
            //Skapa träningspass till datagrid
            
        }

        //Metod för att öppna AddWorkoutWindow
        public void AddWorkOut(object parameter)
        {
            MessageBox.Show("Här öppnas AddWorkoutWindow");
        }

        //Metod för att ta bort ett träningspass
        public void RemoveWorkOut(object parameter)
        {
            MessageBox.Show("Här tar man bort träningspass");
        }

        //Metod för att öppna WorkoutDetailsWindow
        public void OpenDetails(object parameter) //OBS ÄNDRA TILLBAKA TILL (Workout workout), ändrat nu för att det inte går att köra kod annars
        {
                MessageBox.Show("Här öppnas WorkoutDetailWindow");
        }

        //Metod för att logga ut
        public void SignOut(object parameter)
        {
            //Stänger WorkoutWindow och går tillbaka till MainWindow
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            //Stänger ner WorkoutsWindow
            Application.Current.Windows.OfType<WorkoutsWindow>().FirstOrDefault()?.Close();
        }

        //Metod för att öppna UserDetailsWindow
        public void OpenUserWindow(object parameter)
        {
            MessageBox.Show("Här öppnas UserDetailsWindow");
        }

        //Metod för att visa info om företaget
        public void ShowInfo(object parameter)
        {
            MessageBox.Show("Här står lite info om Fit-track");
        }



    }
}
