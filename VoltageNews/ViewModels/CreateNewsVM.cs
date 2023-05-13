using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VoltageNews.Helpers;
using VoltageNews.Models;
using VoltageNews.Views;

namespace VoltageNews.ViewModels
{
    class CreateNewsVM : ObservableObject
    {
        private static string _filePath { get; set; }
        private static string uploadUri { get; set; } = "Not uploaded";
        private static string title { get; set; }
        private static string category { get; set; }
        private static string shortDescription { get; set; }
        private static string articleText { get; set; }
        private Visibility loadVisibility { get; set; } = Visibility.Collapsed;
        private static RelayCommand addImageCommand;
        private static RelayCommand createPost;
        private static RelayCommand cancelBtnClick;
       


        public string Title
        {
            get { return title; }
            set
            {
                title = value;
            }
        }
        public string ShortDescription
        {
            get { return shortDescription; }
            set
            {
                shortDescription = value;
            }
        }
        public string ArticleText
        {
            get { return articleText; }
            set
            {
                articleText = value;
            }
        }
        public string UploadUri
        {
            get { return uploadUri; }
            set { 
                uploadUri = value;
                OnPropertyChanged();
            }
        }
        public string Category
        {
            get { return category; }
            set
            {
                category = value.Split(": ")[1];
            }
        }
        public Visibility LoadVisibility
        {
            get
            {
                return loadVisibility;
            }
            set
            {
                loadVisibility = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddImageCommand
        {
            get
            {
                return addImageCommand ?? (addImageCommand = new RelayCommand(async r =>
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";

                    if (fileDialog.ShowDialog() == true)
                    {
                        
                        _filePath = fileDialog.FileName;
                        UploadUri = fileDialog.SafeFileName;
                    }
                }));
            }
        }

        public RelayCommand CreatePost
        {
            get
            {
                return createPost ?? (new RelayCommand(async r =>
                {
                    if (title?.Length < 10 || shortDescription?.Length < 10 || articleText?.Length < 10 || category?.Length < 1 || _filePath == null)
                    {
                        title = "";
                        MessageBox.Show("Not all fields is correct now.");
                        return;
                    }
                    LoadVisibility = Visibility.Visible;
                    OnPropertyChanged();
                    string imageLink = await uploadToImgur();
                    if(imageLink == "Error")
                    {
                        MessageBox.Show("Some error occured... Try another image...");
                        return;
                    }
                    string result = await AddRawToDb(imageLink);
                    loadVisibility = Visibility.Collapsed;
                    MessageBox.Show(result);
                }));
            }
        }
        public RelayCommand CancelBtnClick
        {
            get
            {
                return cancelBtnClick ?? (cancelBtnClick = new RelayCommand(r => goToHomepage()));
            }
        }

        public async static Task<string> uploadToImgur()
        {
            try
            {
                ImgurSharp.Imgur im = new("4c52e89be4366c0");

                    using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(_filePath)))
                    {
                        ImgurSharp.Image image = await im.UploadImageAnonymous(ms, RandomStringGen.RandomGen() + Path.GetExtension(_filePath), "title", "description");
                        return image.Link;
                    }
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }

        public async static Task<string> AddRawToDb(string imageLink)
        {

            try { 
                using(VoltageDbContext db = new())
                {
                    Article article = new(title, shortDescription, articleText, UserStore.User.Id, imageLink);
                    article.Categories.Add(db.Categories.First(r => r.Title == category));
                    db.Articles.Add(article);
                    await db.SaveChangesAsync();
                    clearValues();
                    goToHomepage();
                    return "All posts has been succesfully created!";
                }
            }
            catch(Exception ex)
            {
                return "Ooops... Try later...";
            }
        }

        public static void clearValues()
        {
            title = shortDescription = articleText = _filePath = "";
        }

        public static void goToHomepage()
        {
            PageManager.helpFrame?.Navigate(new HomePage());
            PageManager.helpFrame?.RemoveBackEntry();
        }
    }
}
