using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF2022User01Lib
{
    public class Calculations
    {
        public string[] AvailablePeriods(TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime)
        {
            if (startTimes.Length == 0 || durations.Length == 0)
            {
                throw new Exception("one of the lists is empty");
            }

            if (startTimes.Length > durations.Length || startTimes.Length < durations.Length)
            {
                throw new Exception("different array lengths between start time and duration");
            }

            if (beginWorkingTime > endWorkingTime)
            {
                throw new Exception("the start time is greater than the end time");
            }

            if (consultationTime <= 0)
            {
                throw new Exception("consultation time is negative or equal to zero");
            }

            List<string> result = new List<string>();
            TimeSpan currentTime = beginWorkingTime;

            for (int i = 0; i <= startTimes.Length; i++)
            {
                TimeSpan availPeriod;
                if (i == startTimes.Length)
                {
                    availPeriod = endWorkingTime - currentTime;
                }
                else
                {
                    availPeriod = startTimes[i] - currentTime;
                }

                if (availPeriod.TotalMinutes >= consultationTime)
                {
                    int iterations = (int) availPeriod.TotalMinutes / consultationTime;
                    for(int j = 0; j < iterations; j++)
                    {
                        TimeSpan currentPeriod = currentTime + TimeSpan.FromMinutes(consultationTime);
                        result.Add(currentTime.ToString("hh\\:mm") + "-" + currentPeriod.ToString("hh\\:mm"));
                        currentTime = currentPeriod;
                    }
                }

                if (i >= startTimes.Length)
                {
                    break;
                }
                else
                {
                    currentTime = startTimes[i].Add(TimeSpan.FromMinutes(durations[i]));
                }
            }
            return result.ToArray();
        }
    }
}
