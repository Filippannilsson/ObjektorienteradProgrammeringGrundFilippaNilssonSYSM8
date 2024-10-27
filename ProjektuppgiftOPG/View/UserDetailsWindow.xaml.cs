﻿using ProjektuppgiftOPG.ViewModel;
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
    public partial class UserDetailsWindow : Window
    {
        public UserDetailsWindow(string selectedQuestion, string answerInput)
        {
            InitializeComponent();
            DataContext = new UserDetailsWindowViewModel(selectedQuestion, answerInput);
        }
    }
}
