using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using VoltageNews.Helpers;
using VoltageNews.Models;
using VoltageNews.Views;

namespace VoltageNews.ViewModels
{
    internal class SoloPostVM : ObservableObject
    {
        private Article article { get; set; }
        private string authorName { get; set; }
        private RelayCommand deleteArticle { get; set; }
        private RelayCommand editArticle { get; set; }
        private List<Comment> comments { get; set; }

        public string AuthorName
        {
            get { return authorName; }
            set { authorName = value; OnPropertyChanged(); }
        }

        public Article Article
        {
            get => article;
            set
            {
                article = value;
                OnPropertyChanged();
            }
        }

        public List<Comment> Comments
        {
            get => comments;
            set
            {
                comments = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand DeleteArticle
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
                            PageManager.helpFrame?.Navigate(new HomePage());
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
        public RelayCommand EditArticle
        {
            get
            {
                return editArticle ?? (editArticle = new RelayCommand(r =>
                {
                    PageManager.helpFrame?.Navigate(new EditPage(Article.NewsId));
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
