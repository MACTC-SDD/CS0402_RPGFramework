using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS0402_RPGFramework
{
    public sealed class GameState
    {
        // Static Fields and Properties
        private static readonly Lazy<GameState> _instance = new Lazy<GameState>(() => new GameState());

        public static GameState Instance => _instance.Value;

        // Fields
        private bool _isRunning = false;

        #region --- Properties ---
        // All players are loaded into this dictionary, with the player's name as the key
        public Dictionary<string, Player> Players { get; set; } = new Dictionary<string, Player>();

        // The telnet server that the game uses
        public TelnetServer? TelnetServer { get; private set; }
        #endregion

        // Constructors
        private GameState() { }

        #region --- Instance Methods ---
        public void AddPlayer(Player player)
        {
            // If the player is already in the dictionary, don't add them again
            if (Players.ContainsKey(player.Name))
            {
                return;
            }

           Players.Add(player.Name, player);
        }

        private void LoadAllAreas()
        {
            // Load all area files
            throw new NotImplementedException("LoadAllAreas() not implemented yet");
        }

        private void LoadAllPlayers()
        {
            // Load all player files
            throw new NotImplementedException("LoadAllPlayers() not implemented yet");
        }

        private void SaveAllAreas()
        {
            // Save all area files
            throw new NotImplementedException("SaveAllAreas() not implemented yet");
        }

        private void SaveAllPlayers()
        {
            // Save all player files
            throw new NotImplementedException("SaveAllPlayers() not implemented yet");
        }

        private void SavePlayer(Player player)
        {
            // Save a player file
            throw new NotImplementedException("SavePlayer() not implemented yet");
        }

        /// <summary>
        /// Do all the tasks necessary to start the game
        /// </summary>
        public async Task Start()
        {
            // LoadAllAreas();
            // LoadAllPlayers();

            this.TelnetServer = new TelnetServer(5555);
            await this.TelnetServer.StartAsync();
            
            _isRunning = true;
        }

        /// <summary>
        /// Do all the tasks necessary to stop the game
        public void Stop()
        {
            // SaveAllAreas();
            // SaveAllPlayers();

            this.TelnetServer?.Stop();
            _isRunning = false;
        }
        #endregion --- Instance Methods ---
    }
}
