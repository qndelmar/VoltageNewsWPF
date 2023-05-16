using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using VoltageNews.Helpers;
using VoltageNews.Models;

namespace VoltageNews.ViewModels
{
    internal class SoloPostVM : ObservableObject
    {
        private Article article { get; set; }

        public Article Article
        {
            get => article;
            set
            {
                article = value;
                OnPropertyChanged();
            }
        }
        public void Init(int id)
        {
            Article = Article.GetOnePost(id);
        }
    }
}
