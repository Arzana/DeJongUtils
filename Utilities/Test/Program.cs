namespace Test
{
    using Mentula.Utilities;
    using Mentula.Utilities.Logging;
    using System;

    public static class Program
    {
        private static ConsoleLogger logger;

        public static void Main(string[] args)
        {
            logger = new ConsoleLogger() { AutoUpdate = true, DynamicPadding = true };

            Log.Debug(nameof(Program), Console.ReadLine());
            Utils.PressAnyKeyToContinue();
        }
    }
}