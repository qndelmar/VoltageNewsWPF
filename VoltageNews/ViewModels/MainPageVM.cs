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
                        PageManager.helpFrame?.Navigate(new HomePage());
                        break;
                    }
                case ("Профиль"):
                    {
                        PageManager.helpFrame?.Navigate(new Account());
                        break;
                    }
                case ("Настройки"):
                    {
                        PageManager.helpFrame?.Navigate(new Settings());
                        break;
                    }
                case ("Статистика"):
                    {
                        PageManager.helpFrame?.Navigate(new Graphs());
                        break;
                    }
                case ("Добавить новость"):
                    {
                        PageManager.helpFrame?.Navigate(new CreateNews());
                        break;
                    }
                case ("Поддержка пользователей"):
                    {
                        PageManager.helpFrame?.Navigate(new Support());
                        break;
                    }
                default:break;
            }
        }
    }
}
