using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using VoltageNews.ViewModels;
using Microsoft.Office.Interop.Word;
using System.Reflection;

namespace VoltageNews.Views
{
    /// <summary>
    /// Interaction logic for SoloPost.xaml
    /// </summary>
    public partial class SoloPost : System.Windows.Controls.Page
    {
        public SoloPost(int id)
        {
            InitializeComponent();
            (DataContext as SoloPostVM)?.Init(id);
        }

        private void printBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PrintDialog print = new();
            if(print.ShowDialog() == true)
            {
                print.PrintVisual(this, "Печать новости");
            }
        }

        private void createDocBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            object oMissing = System.Reflection.Missing.Value;
            var application = new Application();

            Document doc = application.Documents.Add();
            Paragraph author = doc.Paragraphs.Add();
            Microsoft.Office.Interop.Word.Range authorRange = author.Range;
            authorRange.Text = "Автор статьи: " + AuthorName.Text;
            author.set_Style("Normal");
            authorRange.Bold = 500;
            authorRange.InsertParagraphAfter();
            Paragraph views = doc.Paragraphs.Add();
            Microsoft.Office.Interop.Word.Range viewsRange = views.Range;
            viewsRange.Text = "Количество просмотров: " + (DataContext as SoloPostVM)?.Article.Views;
            views.set_Style("Normal");
            viewsRange.Bold = 500;
            viewsRange.InsertParagraphAfter();
            Paragraph comments = doc.Paragraphs.Add();
            Microsoft.Office.Interop.Word.Range commRange = views.Range;
            commRange.Text = "Количество комментариев: " + (DataContext as SoloPostVM)?.Article.Comments.Count;
            commRange.Bold = 500;
            commRange.InsertParagraphAfter();
            Paragraph paragraph = doc.Paragraphs.Add();
            Paragraph image = doc.Paragraphs.Add();
            Microsoft.Office.Interop.Word.Range userRange = paragraph.Range;
            userRange.Text = Title.Text;
            paragraph.set_Style("Title");
            userRange.InsertParagraphBefore();
            object o_CollapseEnd = WdCollapseDirection.wdCollapseEnd;

            Paragraph article = doc.Paragraphs.Add();
            Microsoft.Office.Interop.Word.Range mainTextRng = article.Range;
            mainTextRng.Text = mainText.Text;
            article.set_Style("Normal");
            mainTextRng.InsertParagraphAfter();
            Microsoft.Office.Interop.Word.Range imgrng = doc.Content;
            imgrng.Collapse(ref o_CollapseEnd);
            imgrng.InlineShapes.AddPicture(img.Source.ToString(), oMissing, oMissing, imgrng);
            application.Visible = true;
        }
    }
}
