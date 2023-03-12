using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge
{
    public class LongMessage : AbstractMessage
    {
        public LongMessage(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }
        public override void sendMessage(string message)
        {
            if(message.Length >= 10)
                _messageSender.SendMessage(message);
            else
                Console.WriteLine("The message is too short to be sent as a Long Message.");
        }
    }
}
