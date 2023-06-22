using OpenAI_API.Chat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VoltageNews.Helpers
{
    class OpenAiMethods
    {
        public string ArticleText { get; set; }
        public int CategoryID {
            get; set;
        }
        public async Task<int> getTextFromChatGPT(string ShortDescription)
        {
            if (string.IsNullOrEmpty(ShortDescription))
            {
                MessageBox.Show("Сначала укажите короткое описание статьи, потом будет доступна автогенерация текста");
                return 500;
            }
            try
            {
                var api = new OpenAI_API.OpenAIAPI("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");
                var result = await api.Chat.CreateChatCompletionAsync(new ChatRequest
                {
                    Model = OpenAI_API.Models.Model.ChatGPTTurbo,
                    Temperature = 0.1,
                    MaxTokens = 200,
                    Messages = new ChatMessage[] {
                    new ChatMessage(ChatMessageRole.User, "Generate news article's text for this short description:" + ShortDescription)}
                });
                var category = await api.Chat.CreateChatCompletionAsync(new ChatRequest
                {
                    Model = OpenAI_API.Models.Model.ChatGPTTurbo,
                    Temperature = 0.1,
                    MaxTokens = 1,
                    Messages = new ChatMessage[] {
                    new ChatMessage(ChatMessageRole.User, "What category you can add for this short description: " + ShortDescription + ". You need to send me 0 if category will be Breaking News, 1 - if Entertainment, and 2 if Politics. Only number in message pls.")}
                });
                this.ArticleText = result.ToString();
                this.CategoryID = Convert.ToInt32(category.ToString());
                return 200;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 500;
            }
        }
    }
}
