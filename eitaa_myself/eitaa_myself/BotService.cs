using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Net.WebRequestMethods;

namespace eitaa_myself
{
  
    class BotService
    {
        private readonly string _token;
        private readonly string _chatId;
        
       

        public BotService(string token, string chatId)
        {
            _token = token;
            _chatId = chatId;
        
        }

        public async Task SendMessageAsync(string message)
        {
            string url = $"https://eitaayar.ir/api/{_token}/sendMessage?chat_id={_chatId}&text={Uri.UnescapeDataString(message)}&date=0&parse_mode=&pin=&viewCountForDelete=";


            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string responseText = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Server Response:{responseText}");
            }

        }
        public async Task SendFileAsync(string filePath)
        {
            string url = $"https://eitaayar.ir/api/{_token}/sendFile";

            if (!System.IO.File.Exists(filePath))
            {
                Console.WriteLine("Error: file not found.");
                return;
            }

            using (HttpClient client = new HttpClient())
            using (MultipartFormDataContent form = new MultipartFormDataContent())
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (StreamContent fileContent = new StreamContent(fs))
            {
                form.Add(new StringContent(_chatId), "chat_id");
                form.Add(new StringContent("Sending a file"), "title");
                form.Add(new StringContent("here is your file"), "caption");
                form.Add(fileContent, "file", Path.GetFileName(filePath));

                HttpResponseMessage response = await client.PostAsync(url, form);
                string responseText = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Server Response:{responseText}");
            }
        }

    }



}

