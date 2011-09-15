using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;

namespace SignalRSandbox
{
    public class Chat : Hub
    {
        public void Send(string message)
        {

            //var name = Caller.name;
            Clients.addMessage("Coordinator", message);
        }

        public void SendAnonymous(string message)
        {
            Clients.addMessage("Coordinator", message);
        }
    }
}