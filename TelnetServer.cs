using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CS0402_RPGFramework
{
    public class TelnetServer
    {
        // Fields
        private TcpListener _listener;
        private bool _isRunning;

        public TelnetServer(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
        }

        /// <summary>
        /// Listen for incoming connections and create a new thread to handle each one
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            _listener.Start();
            _isRunning = true;
            IPEndPoint localEndPoint = (IPEndPoint)_listener.LocalEndpoint;
            Console.WriteLine($"Telnet Server is running on port {localEndPoint.Port}...");

            while (_isRunning)
            {
                TcpClient client = await _listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        }

        /// <summary>
        /// Handle input and output for a single client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                Player player = new Player(client);

                
                await player.Network.Writer.WriteLineAsync("MOTD");
                Console.WriteLine("New client connected!");

                try
                {
                    // Keep listening for commands as long as player is connected
                    while (client.Connected)
                    {
                        string? command = await player.Network.Reader.ReadLineAsync();
                        if (command == null)
                            break;

                        Console.WriteLine($"Received command: {command}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection error: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("Client disconnected");
                }
            } // end using client
        }

        /// <summary>
        /// Things that need to happen when TelnetServer is stopped
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
            _listener.Stop();
        }

    }
}
