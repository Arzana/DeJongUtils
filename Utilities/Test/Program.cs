namespace Test
{
    using DeJong.Utilities.Logging;
    using System;

    public static class Program
    {
        public static void Main(string[] args)
        {
            ConsoleLogger cl = new ConsoleLogger();
            {
                Log.Debug("Test");
                cl.Update();
                Console.ReadKey();
            }
        }
    }
}