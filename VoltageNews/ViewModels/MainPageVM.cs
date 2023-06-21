using HandyControl.Controls;
using VoltageNews.Helpers;
using VoltageNews.Views;

namespace VoltageNews.ViewModels
{
    internal class MainPageVM : ObservableObject
    {
        private int selectedItem { get; set; }
        public static HomePage homepage = new HomePage();

        private RelayCommand? switchItemCmd;

        public RelayCommand SwitchItemCmd
        {
            get { return switchItemCmd ??
                    (switchItemCmd = new RelayCommand(f =>
                    {
                        var currentItem = (f as HandyControl.Data.FunctionEventArgs<object>).Info as SideMenuItem;
                        changePageByNumber(currentItem?.Header.ToString());
                    }));    
                }
        }

        public void changePageByNumber(string? header)
        {
            switch (header)
            {
                case ("Главная"):
                    {
                        PageManager.helpFrame?.Navigate(homepage);
                        PageManager.helpFrame?.RemoveBackEntry();
                        break;
                    }
                case ("Редактирование прав"):
                    {
                        PageManager.helpFrame?.Navigate(new AccountManagement());
                        PageManager.helpFrame?.RemoveBackEntry();
                        break;
                    }
                case ("Статистика"):
                    {
                        PageManager.helpFrame?.Navigate(new Graphs());
                        PageManager.helpFrame?.RemoveBackEntry();
                        break;
                    }
                case ("Добавить новость"):
                    {
                        PageManager.helpFrame?.Navigate(new CreateNews());
                        PageManager.helpFrame?.RemoveBackEntry();
                        break;
                    }
                case ("История входов"):
                    {
                        PageManager.helpFrame?.Navigate(new LoginHistory());
                        PageManager.helpFrame?.RemoveBackEntry();
                        break;
                    }
                default:break;
            }
        }
    }
}
