using HandyControl.Controls;
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
        private RelayCommand searchItems { get; set; }
        private static string searchText { get; set; }
        private static string sortText { get; set; }
        private int pageIndex { get; set; }
        public int PageIndex
        {
            get
            {
                return pageIndex;
            }
            set
            {
                pageIndex = value;
                OnPropertyChanged();
            }
        }

        public string SortText
        {
            get
            {
                return sortText;
            }
            set
            {
                sortText = value; OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Article> Data
        {
            get { return data; }
            set { data = value; OnPropertyChanged(); }
        }

        public RelayCommand SearchItems
        {
            get
            {
                return searchItems ?? (searchItems = new RelayCommand(r =>
                {
                    if (searchText == "" || searchText == null)
                    {
                        this.Init();
                        return;
                    }
                    if(r != null)
                    {
                        Data = new ObservableCollection<Article>(Article.SearchItems(searchText, Convert.ToInt32(r), 9));
                        return;
                    }
                    Data = new ObservableCollection<Article>(Article.SearchItems(searchText, 1, 9));
                    PageIndex = 1;
                    this.ComputePagesAmount();
                })
                {

                });
            }
        }
        
        public void Init()
        {
            Data = new ObservableCollection<Article>(Article.GetFixedAmount(1, 9));
            double articlesCount = Article.GetCount();
            double pagesCount = articlesCount / 9;
            PagesAmount = Convert.ToInt32(Math.Ceiling(pagesCount));
            PageIndex = 1;
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
            if (searchText != null && searchText.Length > 0)
            {
                this.SearchItems.Execute(pageNum);
                return;
            }
            if(sortText != null && sortText.Split(": ")[1] != "Default")
            {
                Data = new ObservableCollection<Article>(Article.GetSortedArticlesByPageNumber(SortText.Split(": ")[1], pageNum, amount));
                return;
            }
            Data = new ObservableCollection<Article>(Article.GetFixedAmount(pageNum, amount));
        }
        public void SortData(string sortType)
        {
            Data = new ObservableCollection<Article>(Article.GetSortedArticlesByPageNumber(sortType, 1, 9));
        }
        public void ComputePagesAmount()
        {
            double articlesCount = Article.GetItemsCount(searchText);
            double pagesCount = articlesCount / 9;
            PagesAmount = Convert.ToInt32(Math.Ceiling(pagesCount));
        }
    }
}
