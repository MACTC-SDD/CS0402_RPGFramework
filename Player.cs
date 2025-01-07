using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CS0402_RPGFramework
{
    public class Player : Character
    {
        public PlayerNetwork Network { get; set; }

        public Player(TcpClient client)
        {
            Network = new PlayerNetwork(client);
        }

        /// <summary>
        /// Sends a message to the player 
        /// </summary>
        /// <param name="message"></param>
        public void WriteLine(string message)
        {
            Network.Writer.WriteLine(message);
        }
    }
}
