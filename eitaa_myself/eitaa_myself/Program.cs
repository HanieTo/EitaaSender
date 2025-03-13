using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace eitaa_myself
{
    class Program
    {

        static async Task Main()
        {


            Console.WriteLine("Please choose: \n1 .send text \n 2.send file \n type 1 or 2");
            string Choose = Console.ReadLine();


            Console.WriteLine("Write your token");
            string token = Console.ReadLine();

            Console.WriteLine("Write your chat id");
            string chatId = Console.ReadLine();


            BotService botService = new BotService(token, chatId);


            if (Choose == "1")
            {
                Console.WriteLine("Write your message");
                string message = Console.ReadLine();

                await botService.SendMessageAsync(message);
            }
            else if (Choose == "2")
            {

                Console.WriteLine("Write your filePath");
                string filePath = Console.ReadLine();

                await botService.SendFileAsync(filePath);

            }
            else
            {
                Console.WriteLine("Invalid choice. Please restart the program.");
            }

        }



    }
}

