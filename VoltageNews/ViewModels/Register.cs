using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoltageNews.Helpers;

namespace VoltageNews.ViewModels
{
    internal class Register : ObservableObject
    {
        private static string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                changeSuggestions();
                OnPropertyChanged();
            }
        }

        private static ObservableCollection<string> suggestions = new() { "@gmail.com", "@yandex.ru", "@outlook.com", "@mail.ru" };

        public ObservableCollection<string> AllSuggestions
        {
            get
            {
                return suggestions;
            }
        }

        public static void changeSuggestions()
        {
            if (email.Contains("@"))
            {
                suggestions.Clear();
                return;
            }
            if (suggestions.Count == 0)
            {
                suggestions.Add("@gmail.com");
                suggestions.Add("@yandex.ru");
                suggestions.Add("@outlook.com");
                suggestions.Add("@mail.ru");
            }
            suggestions[0] = email + "@gmail.com";
            suggestions[1] = email + "@yandex.ru";
            suggestions[2] = email + "@outlook.com";
            suggestions[3] = email + "@outlook.com";
        }
    }
}
