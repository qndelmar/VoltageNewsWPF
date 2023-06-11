using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VoltageNews.Helpers;

namespace VoltageNews.Views;

public partial class Auth : Page
{
    ViewModels.Auth auth = new ViewModels.Auth();
    public Auth()
    {
        InitializeComponent();
    }

    private void passwordBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (this.DataContext != null)
        {
            if(this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((HandyControl.Controls.PasswordBox)sender).Password; }
        }
    }

    private async void submitBtn_Click(object sender, System.Windows.RoutedEventArgs e)
    {
       int result = await auth.SignIn();
        if(result == 404)
        {
            emailBox.BorderBrush = new SolidColorBrush(Colors.Red);
            errorTextBlock.Text = "Похоже, такого аккаунта не существует..";
            return;
        }
        if(result == 401)
        {
            errorTextBlock.Text = "Неправильный логин или пароль";
            passwordBox.BorderBrush = new SolidColorBrush(Colors.Red);
            return;
        }
        if(result == 500)
        {
            errorTextBlock.Text = "Something went wrong..";
            emailBox.BorderBrush = new SolidColorBrush(Colors.Red);
            return;
        }
        PageManager.frame.Navigate(new MainPage());
        ChangeWindowSize.ChangingWindowSize(1100, 650, MainWindow.GetWindow(this));
    }

    private void boxes_GotFocus(object sender, System.Windows.RoutedEventArgs e)
    {
        errorTextBlock.Text = "";
        emailBox.BorderBrush = new SolidColorBrush(Colors.LightGray);
        passwordBox.BorderBrush = new SolidColorBrush(Colors.LightGray);
    }

    private void TextBlock_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        PageManager.frame?.Navigate(new Register());
    }

    private void TextBlock_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        (sender as TextBlock).TextDecorations = TextDecorations.Underline;
    }

    private void TextBlock_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        (sender as TextBlock).TextDecorations = null;
    }
}