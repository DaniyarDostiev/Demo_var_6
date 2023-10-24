using SF2022User01Lib;

namespace TestMain
{
    internal class TestClass
    {
        
        public static void Main(string[] args)
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10, 00, 00),
                new TimeSpan(11, 00, 00),
                new TimeSpan(15, 00, 00),
                new TimeSpan(15, 30, 00),
                new TimeSpan(16, 50, 00)
            };

            int[] durations = new int[]
                {
                60, 30, 10, 10, 40
                };

            TimeSpan beginWorkingTime = new TimeSpan(8, 00, 00);
            TimeSpan endWorkingTime = new TimeSpan(18, 00, 00);
            int consultationTime = 30;

            Calculations calculations = new Calculations();
            string[] strings = calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            for (int i = 0; i < strings.Length; i++)
            {
                Console.WriteLine(strings[i]);
            }
        }
    }
    
}
