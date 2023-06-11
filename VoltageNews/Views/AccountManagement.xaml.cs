﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VoltageNews.ViewModels;

namespace VoltageNews.Views
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class AccountManagement : Page
    {
        public AccountManagement()
        {
            InitializeComponent();
            (DataContext as AccountManagementVM).Category = "Пользователь";
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            (DataContext as AccountManagementVM).Category = (sender as ComboBox).Text;
        }
    }
}