﻿using HandyControl.Controls;
using System;
using VoltageNews.Helpers;
using VoltageNews.Models;

namespace VoltageNews.ViewModels
{
    internal class AccountManagementVM : ObservableObject
    {
        private string error { get; set; }
        private string uid { get; set; }
        private string category { get; set; }
        private RelayCommand editUserPermissions
        {
            get;set;
        }

        public string Error { get => error; set
            {
                error = value;
                OnPropertyChanged();
            } 
        }
        public string Uid
        {
            get => uid; set
            {
                uid = value;
                OnPropertyChanged();
            }
        }
        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand EditUserPermissions
        {
            get
            {
                return editUserPermissions ?? (editUserPermissions = new RelayCommand(r =>
                {
                    try
                    {
                        if(string.IsNullOrEmpty(uid))
                        {
                            MessageBox.Show("Введите userID");
                            return;
                        }
                        bool result = User.editUserPermission(Convert.ToInt32(uid), Category);
                        PageManager.helpFrame?.Navigate(MainPageVM.homepage);
                        PageManager.helpFrame?.RemoveBackEntry();
                    }
                    catch(Exception ex)
                    {
                        if(ex.Message == "This user is not exists")
                        {
                            MessageBox.Show("Oops... Try another user");
                            return;
                        }
                        if(ex.Message == "Incorrect role")
                        {
                            MessageBox.Show("Role in ComboBox is not correct");
                            return;
                        }
                        MessageBox.Show("Something went wrong");
                    }
                }));
            }
        }
    }
}
