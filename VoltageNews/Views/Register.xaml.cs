using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VoltageNews.Helpers;
using VoltageNews.Models;
using VoltageNews.ViewModels;

namespace VoltageNews.Views;

public partial class Register : Page
{ 

    public Register()
    {
        InitializeComponent();
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

    private async void submitButton_Click(object sender, RoutedEventArgs e)
    {
        if(emailBox.Text.Length < 5 || nicknameBox.Text.Length == 0) {
            System.Windows.MessageBox.Show("Не все поля заполнены верно", "Error");
            return;
        }
        User user = new User(nicknameBox.Text, emailBox.Text, passwordBox.Password);
        loadingVisibility.Visibility = Visibility.Visible;
        bool result = await user.createUser();
        loadingVisibility.Visibility = Visibility.Hidden;
        if (result)
        {
            submitButton.Background = new SolidColorBrush(Colors.Green);
            Thread.Sleep(300);
            PageManager.frame.Navigate(new MainPage());
            ChangeWindowSize.ChangingWindowSize(1100, 650, MainWindow.GetWindow(this));
        }
    }

    private async void emailBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if(emailBox.Text.Length < 5)
        {
            return;
        }
        int result = await User.checkIfExists(emailBox.Text);
        if (emailBox.Text.Length > 5 && result == 200)
        {
            emailTip.Text = "Great! Your email doesn't exist.";
            emailTip.Foreground = new SolidColorBrush(Colors.DarkGreen);
            emailBox.BorderBrush = new SolidColorBrush(Colors.DarkGreen);
            emailTip.Visibility = Visibility.Visible;
        }
        else
        {
            emailTip.Text = "Oh... This email is already registered.";
            emailTip.Foreground = new SolidColorBrush(Colors.Red);
            emailBox.BorderBrush = new SolidColorBrush(Colors.Red);
            emailTip.Visibility = Visibility.Visible;
        }
    }

    private void emailBox_GotKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
    {
        emailTip.Visibility = Visibility.Collapsed;
        emailBox.BorderBrush = new SolidColorBrush(Colors.Gray);
    }

    private void TextBlock_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        PageManager.frame?.GoBack();
    }
}


