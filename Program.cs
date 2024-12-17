namespace CS0402_RPGFramework
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            TelnetServer server = new TelnetServer(5555);

            await server.StartAsync();
        }
    }
}
