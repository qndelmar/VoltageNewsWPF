﻿using VoltageNews.Helpers;
using VoltageNews.Models;
using VoltageNews.Views;

namespace VoltageNews.ViewModels
{
    class EditPageVM : ObservableObject
    {
        private Article article { get; set; }
        private RelayCommand submitCmd { get; set; }

        public RelayCommand SubmitCmd
        {
            get
            {
                return submitCmd ?? (submitCmd = new RelayCommand(r =>
                {
                   bool result = Article.editArticle(article.Title, article.ShortDescription, article.ArticleText, article.NewsId);
                    if(result == true)
                    {
                        MainPageVM.homepage = new HomePage();
                        PageManager.helpFrame?.Navigate(MainPageVM.homepage);
                        PageManager.helpFrame?.RemoveBackEntry();
                    }
                }));
            }
        }

        public Article Article
        {
            get => article;
            set {
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
