using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge
{
    public class EmailMessageSender : IMessageSender
    {
        public void SendMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
