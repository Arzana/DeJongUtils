namespace Test
{
    using DeJong.Utilities;
    using DeJong.Utilities.Logging;
    using System;

    public static class Program
    {
        public static void Main(string[] args)
        {
            using (ConsoleLogger logger = new ConsoleLogger { AutoUpdate = true, DynamicPadding = true })
            {
                Log.Debug(nameof(Program), Console.ReadLine());
                Utils.PressAnyKeyToContinue();
            }
        }
    }
}