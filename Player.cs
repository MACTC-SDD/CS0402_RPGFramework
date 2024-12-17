using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CS0402_RPGFramework
{
    public class Player
    {
        public string Name { get; set; } = "No Name Set";
        public PlayerNetwork Network { get; set; }

        public Player(TcpClient client)
        {
            Network = new PlayerNetwork(client);
        }
    }
}
