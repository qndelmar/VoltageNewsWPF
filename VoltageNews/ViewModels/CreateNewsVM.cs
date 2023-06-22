using Microsoft.Win32;
using OpenAI_API.Chat;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using VoltageNews.Helpers;
using VoltageNews.Models;

namespace VoltageNews.ViewModels
{
    class CreateNewsVM : ObservableObject
    {
        
        private static string? _filePath { get; set; }
        private static string uploadUri { get; set; } = "Not uploaded";
        private static string? title { get; set; }
        private static string? category { get; set; }
        private static string? shortDescription { get; set; }
        private static string? articleText { get; set; }
        private static int selectedIndex { get; set; }
        private RelayCommand? generateText { get; set; }

        private Visibility loadVisibility { get; set; } = Visibility.Collapsed;
        private static RelayCommand? addImageCommand;
        private static RelayCommand? createPost;
        private static RelayCommand? cancelBtnClick;
       


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
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                selectedIndex = value;
                OnPropertyChanged();
            }
        }
        public string ArticleText
        {
            get { return articleText; }
            set
            {
                articleText = value;
                OnPropertyChanged();
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
                return addImageCommand ?? (addImageCommand = new RelayCommand(r =>
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png;.jpeg";

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
                        MessageBox.Show("Not all fields is correct now.");
                        return;
                    }
                    LoadVisibility = Visibility.Visible;
                    string imageLink = await uploadToImgur();
                    if(imageLink == "Error")
                    {
                        MessageBox.Show("Some error occured... Try another image...");
                        return;
                    }
                    Article article = new(title, shortDescription, articleText, UserStore.User.Id, imageLink);
                    string result = await article.createArticle(category);
                    clearValues();
                    loadVisibility = Visibility.Collapsed;
                    MessageBox.Show(result);
                    goToHomepage();

                }));
            }
        }


        public RelayCommand? GenerateText
        {
            get
            {
                return generateText ?? (generateText = new RelayCommand(async r =>
                {
                    LoadVisibility = Visibility.Visible;
                    OpenAiMethods openAiGenerated = new OpenAiMethods();
                    await openAiGenerated.getTextFromChatGPT(ShortDescription);
                    ArticleText = openAiGenerated.ArticleText;
                    SelectedIndex = openAiGenerated.CategoryID;
                    LoadVisibility = Visibility.Collapsed;
                }));
            }
        }
        public RelayCommand CancelBtnClick
        {
            get
            {
                return cancelBtnClick ??= new RelayCommand(r => goToHomepage());
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
            catch (Exception)
            {
                return "Error";
            }
        }


        public static void clearValues()
        {
            title = shortDescription = articleText = _filePath = "";
        }

        public static void goToHomepage()
        {
            MainPageVM.homepage = new Views.HomePage();
            PageManager.helpFrame?.Navigate(MainPageVM.homepage);
            PageManager.helpFrame?.RemoveBackEntry();
        }


    }
}
