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
        private TcpListener _listener;
        private bool _isRunning;

        public TelnetServer(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
        }

        // Listen for incoming connections
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

        // When a client connects, this method is called
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

        public void Stop()
        {
            _isRunning = false;
            _listener.Stop();
        }

    }
}
