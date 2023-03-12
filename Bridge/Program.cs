using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please select your destiny:");
            int option = int.Parse(Console.ReadLine());
            Console.WriteLine("Insert message:");
            var message = Console.ReadLine();
            switch (option)
            {
                case 1:
                    AbstractMessage shortMessage = new ShortMessage(new SmsMessageSender());
                    shortMessage.sendMessage(message);
                    break;

                case 2:
                    AbstractMessage longMessage = new LongMessage(new EmailMessageSender());
                    longMessage.sendMessage(message);
                    break;

                default:
                    break;
            }
            Console.ReadLine();
        }
    }
}
