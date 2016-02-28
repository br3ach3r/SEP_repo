using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace templateProj.Hubs
{
    
    public class SessionHub : Microsoft.AspNet.SignalR.Hub
    {
        public void TimeoutNotify()
        {
            Clients.All.NewMessage("timeout");
        }
         
    }
}