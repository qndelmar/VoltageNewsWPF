using HandyControl.Controls;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
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
                case ("Профиль"):
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
                case ("Поддержка пользователей"):
                    {
                        PageManager.helpFrame?.Navigate(new Support());
                        PageManager.helpFrame?.RemoveBackEntry();
                        break;
                    }
                default:break;
            }
        }
    }
}
