using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge
{
    public class ShortMessage : AbstractMessage
    {
        public ShortMessage(IMessageSender messageSender)
        {
             _messageSender = messageSender;
        }
        public override void sendMessage(string message)
        {
            _messageSender.SendMessage(message);
        }
    }
}
