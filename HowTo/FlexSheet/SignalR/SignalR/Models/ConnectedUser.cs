using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR.Models
{
    public class ConnectedUser
    {
        public string ConnectionId { get; set; }
        public string UserId { get; set; }
    }
}