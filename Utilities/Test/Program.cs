namespace Test
{
    using DeJong.Utilities;
    using DeJong.Utilities.Core;
    using DeJong.Utilities.Logging;
    using System;

    public static class Program
    {
        public static void Main(string[] args)
        {
            using (ConsoleLogger logger = new ConsoleLogger { AutoUpdate = true })
            {
                Utils.PressAnyKeyToContinue();
            }
        }
    }
}