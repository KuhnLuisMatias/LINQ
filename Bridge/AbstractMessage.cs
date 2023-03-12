using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge
{
    public abstract class AbstractMessage 
    {
        protected IMessageSender _messageSender;
        public abstract void sendMessage(string message);
    }
}
