using System.Linq;
using System.Runtime.CompilerServices;

namespace SF2022UserNNLib
{
    public class Calculations
    {
        public TimeSpan[] startTimes;
        public TimeSpan beginWorkingTime;
        public TimeSpan endWorkingTime;
        public int[] durations;
        public int consultationTime;

        public Calculations()
        {
            
        }

        public void StartMethod()
        {
            Console.WriteLine("Введите время начала и продолжительность: ");
            string input = Console.ReadLine();
            string[] formInput = input.Split(" ");
            string[] time;
            List<TimeSpan> startTimesList = new List<TimeSpan>();
            List<int> durationsList = new List<int>();
            for (int i = 0; i < formInput.Length; i++)
            {
                if (i % 2 == 1)
                {
                    durationsList.Add(Convert.ToInt32(formInput[i]));
                }
                else
                {
                    time = formInput[i].Split(":");
                    int hours = Convert.ToInt32(time[0]);
                    int minutes = Convert.ToInt32(time[1]);
                    startTimesList.Add(new TimeSpan(hours, minutes, 0));
                }
            }
            startTimes = startTimesList.ToArray();
            durations = durationsList.ToArray();
            Console.WriteLine("Введите рабочий день: ");
            input = Console.ReadLine();
            formInput = input.Split("-");
            time = formInput[0].Split(":");
            beginWorkingTime = new TimeSpan(Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), 0);
            time = formInput[1].Split(":");
            endWorkingTime = new TimeSpan(Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), 0);
            Console.WriteLine("Введите время консультации: ");
            consultationTime = Convert.ToInt32(Console.ReadLine());

            if (startTimes.Length == 0 || durations.Length == 0)
            {
                throw new ArgumentException("Массивы времени начала и продолжительности не могут быть пустыми.");
            }

            if (consultationTime <= 0)
            {
                throw new ArgumentException("Длительность консультации должна быть больше нуля.");
            }

            if (beginWorkingTime >= endWorkingTime)
            {
                throw new ArgumentException("Время начала рабочего дня должно быть раньше времени окончания рабочего дня.");
            }

            for (int i = 0; i < startTimes.Length; i++)
            {
                if (startTimes[i] < beginWorkingTime || startTimes[i] > endWorkingTime)
                {
                    throw new ArgumentException("Обнаружено недопустимое время начала.");
                }

                if (durations[i] <= 0)
                {
                    throw new ArgumentException("Обнаружена недопустимая продолжительность.");
                }
            }

            string[] result = AvailablePeriods();
            foreach (string s in result) { Console.WriteLine(s); }
        }

        public TimeSpan MinTimeSpan(TimeSpan a, TimeSpan b)
        {
            if (a < b) return a;
            else if (b < a) return b;
            return a;
        }

        public string[] AvailablePeriods()
        {
            List<string> periods = new List<string>();
            TimeSpan currentTime = beginWorkingTime;
            int i = 0;
            while (!currentTime.Equals(endWorkingTime))
            {
                if (i >= startTimes.Length || !startTimes.Contains(currentTime) && 
                    currentTime.Add(new TimeSpan(0, consultationTime, 0)) <= startTimes[i])
                {
                    if (currentTime.Add(new TimeSpan(0, consultationTime, 0)) > endWorkingTime)
                    {
                        return periods.ToArray();
                    }
                    periods.Add($"{currentTime}-{MinTimeSpan(currentTime.Add(new TimeSpan(0, consultationTime, 0)), endWorkingTime)}");
                    currentTime = MinTimeSpan(currentTime.Add(new TimeSpan(0, consultationTime, 0)), endWorkingTime);  
                }
                else
                {
                    currentTime = currentTime.Add(new TimeSpan(0, durations[i], 0));
                    i++;
                    if (i < startTimes.Length && currentTime.Add(new TimeSpan(0, consultationTime, 0)) > startTimes[i]
                        && currentTime.Add(new TimeSpan(0, consultationTime, 0)) <= startTimes[i].Add(new TimeSpan(0, durations[i], 0)) && !startTimes.Contains(currentTime))
                    {
                        currentTime = startTimes[i].Add(new TimeSpan(0, durations[i], 0));
                        i++;
                    } else if (i >= startTimes.Length && currentTime.Add(new TimeSpan(0, consultationTime, 0)) > startTimes[i-1])
                    {
                        currentTime = startTimes[i-1].Add(new TimeSpan(0, durations[i-1], 0));
                    }
                }
            }
            return periods.ToArray();
        }


    }
}