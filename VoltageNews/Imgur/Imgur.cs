using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImgurSharp
{
    public class Imgur : IDisposable
    {

        private readonly HttpClient client;
        private const string baseUrl = "https://api.imgur.com/3/";

        public Imgur(string clientId) : this(clientId, null) { }

        public Imgur(string clientId, HttpMessageHandler handler)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new Exception("ClientID is not set, please specify");
            }

            client = handler != null ? new HttpClient(handler) : new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Client-ID " + clientId);
        }
        public async Task<Image> UploadImageAnonymous(Stream imageStream, string name, string title, string description)
        {
            string base64Image = PhotoStreamToBase64(imageStream);

            var jsonData = JsonConvert.SerializeObject(new
            {
                image = base64Image,
                name,
                title,
                description,
                type = "base64"
            });

            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(baseUrl + "upload", jsonContent);

            await CheckHttpStatusCode(response);
            string content = await response.Content.ReadAsStringAsync();

            ResponseRootObject<Image> imgRoot = JsonConvert.DeserializeObject<ResponseRootObject<Image>>(content);

            return imgRoot.Data;
        }

        #region Helpers

        string PhotoStreamToBase64(Stream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                byte[] result = memoryStream.ToArray();
                return Convert.ToBase64String(result);
            }
        }

        private async Task CheckHttpStatusCode(HttpResponseMessage responseMessage)
        {
            var content = await responseMessage.Content.ReadAsStringAsync();
            ResponseRootObject<RequestError> errorRoot = null;

            try
            {
                errorRoot = JsonConvert.DeserializeObject<ResponseRootObject<RequestError>>(content);
            }
            catch (Exception) { }

            if (errorRoot == null)
                return;

            if ((int)responseMessage.StatusCode / 100 > 2)
            {
                throw new ResponseException(string.Format(" Error: {0} \n Request: {1} \n Verb: {2} ", errorRoot.Data.Error, errorRoot.Data.Request, errorRoot.Data.Method));
            }

            return;
        }

        public void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
            }
        }
        #endregion
    }
}