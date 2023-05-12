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

namespace VoltageNews.ViewModels
{
    class CreateNewsVM
    {
        ImgurSharp.Imgur im = new("4c52e89be4366c0");
        private static RelayCommand addImageCommand;
        private static RelayCommand createPost;

        public RelayCommand AddImageCommand
        {
            get
            {
                return addImageCommand ?? (addImageCommand = new RelayCommand(async r =>
                {
                    try
                    {
                        string fileContent = "";
                        OpenFileDialog fileDialog = new OpenFileDialog();
                        fileDialog.InitialDirectory = "c:\\";
                        fileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
                        if (fileDialog.ShowDialog() == true)
                        {
                            string filePath = fileDialog.FileName;
                            using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(filePath)))
                            {
                                ImgurSharp.Image image = await im.UploadImageAnonymous(ms, RandomStringGen.RandomGen()  + Path.GetExtension(filePath), "title", "description");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {

                    }
                }));
            }
        }

        public RelayCommand CreatePost
        {
            get
            {
                return createPost ?? (new RelayCommand(r =>
                {

                }));
            }
        }

        public static string uploadToImgur()
        {
            return "";
        }
    }
}
