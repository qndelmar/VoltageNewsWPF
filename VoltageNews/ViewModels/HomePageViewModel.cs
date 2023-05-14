using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoltageNews.Helpers;
using VoltageNews.Models;

namespace VoltageNews.ViewModels
{
    internal class HomePageViewModel : ObservableObject
    {
        private static ObservableCollection<Article> data { get; set; }
        private int pagesAmount { get; set; }

        public ObservableCollection<Article> Data
        {
            get { return data; }
            set { data = value; OnPropertyChanged(); }
        }
        
        public void Init()
        {
            Data = new ObservableCollection<Article>(Article.GetFixedAmount(1, 9));
            double articlesCount = Article.GetCount();
            double pagesCount = articlesCount / 9;
            PagesAmount = Convert.ToInt32(Math.Ceiling(pagesCount));
        }
        public int PagesAmount
        {
            get {
                return pagesAmount;
            }
            set
            {
                pagesAmount = value;
                OnPropertyChanged();
            }
        }

        public void Pagination(int pageNum, int amount)
        {
            Data = new ObservableCollection<Article>(Article.GetFixedAmount(pageNum, amount));
        }
    }
}
