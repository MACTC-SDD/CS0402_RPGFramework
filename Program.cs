﻿namespace CS0402_RPGFramework
{
    internal class Program
    {

        public static async Task Main(string[] args)
        {
            GameState gameState = GameState.Instance;
            await gameState.Start();
        }
    }
}
