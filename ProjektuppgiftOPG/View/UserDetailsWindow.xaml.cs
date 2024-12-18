﻿using ProjektuppgiftOPG.Model;
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
    public partial class UserDetailsWindow : Window
    {
        public UserDetailsWindow(UserManager userManager, string username)
        {
            InitializeComponent();
            DataContext = new UserDetailsWindowViewModel(userManager, username);
        }
    }
}
