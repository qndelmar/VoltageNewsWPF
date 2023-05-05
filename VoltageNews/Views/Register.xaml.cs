using HandyControl.Controls;
using HandyControl.Tools;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using VoltageNews.Helpers;
using VoltageNews.Models;
using VoltageNews.ViewModels;

namespace VoltageNews.Views;

public partial class Register : Page
{
    Suggestions suggestions = new();
    public Register()
    {
        InitializeComponent();
        suggestions.suggestionsArray = new ObservableCollection<string>() { "@gmail.com", "@yandex.ru", "@outlook.com", "@mail.ru"};
        emailBox.DataContext = suggestions;
    }


    private void emailBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (emailBox.Text.Contains("@") || emailBox.Text.Length == 0)
        {
            emailBox.AutoComplete = false;
            return;
        }
        else
        {
            emailBox.DataContext = suggestions;
        }
        suggestions.suggestionsArray[0] = emailBox.Text + "@gmail.com";
        suggestions.suggestionsArray[1] = emailBox.Text + "@yandex.ru";
        suggestions.suggestionsArray[2] = emailBox.Text + "@outlook.com";
        suggestions.suggestionsArray[3] = emailBox.Text + "@mail.ru";
        suggestions.OnPropertyChanged();
    }

    private void PasswordBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
    {
        DifficultyProgress progress = DifficultyProgress.ValidatePassword(passwordBox.Password);
        passwordTip.Text = progress.errorText;
        difficultyProgress.Value = progress.difficultyProgress;
        if(difficultyProgress.Value == 100)
        {
            submitButton.IsEnabled = true;
        }
        else
        {
            submitButton.IsEnabled = false;
        }
    }

    private void submitButton_Click(object sender, RoutedEventArgs e)
    {
        if(emailBox.Text.Length < 5 || nicknameBox.Text.Length == 0) {
            System.Windows.MessageBox.Show("Не все поля заполнены верно", "Error");
            return;
        }
    }
}

public class Suggestions : ObservableObject
{
    public ObservableCollection<string> suggestionsArray
    {
        get; set;
    }
}
