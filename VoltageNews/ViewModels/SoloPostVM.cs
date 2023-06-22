using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using VoltageNews.Helpers;
using VoltageNews.Models;
using VoltageNews.Views;

namespace VoltageNews.ViewModels
{
    internal class SoloPostVM : ObservableObject
    {
        private Article? article { get; set; }
        private string? authorName { get; set; }
        private RelayCommand? deleteArticle { get; set; }
        private RelayCommand? editArticle { get; set; }
        private List<object>? comments { get; set; }
        private string? newCommentText { get; set; }
        private RelayCommand? createComment { get; set; }
        private RelayCommand? printPage { get; set; }


        public string? AuthorName
        {
            get { return authorName; }
            set { authorName = value; OnPropertyChanged(); }
        }

        public Article? Article
        {
            get => article;
            set
            {
                article = value;
                OnPropertyChanged();
            }
        }

        public List<object>? Comments
        {
            get => comments;
            set
            {
                comments = value;
                OnPropertyChanged();
            }
        }
        public string? NewCommentText
        {
            get => newCommentText;
            set
            {
                newCommentText = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand? DeleteArticle
        {
            get
            {
                return deleteArticle ?? (deleteArticle = new RelayCommand(r =>
                {
                    if(MessageBox.Show("Are you sure?", "Question", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        bool result = Article.deleteArticle(article.NewsId);
                        if (result == true)
                        {
                            MainPageVM.homepage = new HomePage();
                            PageManager.helpFrame?.Navigate(MainPageVM.homepage);
                            PageManager.helpFrame?.RemoveBackEntry();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong");
                        }
                    }
                    else
                    {
                        return;
                    }

                }));
            }
        }
        public RelayCommand? EditArticle
        {
            get
            {
                return editArticle ?? (editArticle = new RelayCommand(r =>
                {
                    PageManager.helpFrame?.Navigate(new EditPage(Article.NewsId));
                }));
            }
        }
        public RelayCommand? CreateComment
        {
            get
            {
                return createComment ?? (createComment = new RelayCommand(r =>
                {
                    bool result = Comment.createComment(article.NewsId, newCommentText);
                    Comments = Comment.getComments(article.NewsId);
                }));
            }
        }

        public void Init(int id)
        {
            Article = Article.GetOnePost(id);
            AuthorName = Editor.getName(article.Author);
            Comments = Comment.getComments(id);
            
        }
    }
}
