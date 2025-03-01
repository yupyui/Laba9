using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
        public class DialClockArray
        {

        public void SetTimeManually()
            {
                Console.Write("Введите часы (0-23): ");
                Hours = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите минуты (0-59): ");
                Minutes = Convert.ToInt32(Console.ReadLine());
            }

            private int hours;
            private int minutes;
            private static int objectCount = 0;

            public int Hours
            {
                get => hours;
                set => hours = (value >= 0 && value <= 23) ? value : throw new ArgumentOutOfRangeException("Часы", "Часы должны быть между 0 и 23.");
            }

            public int Minutes
            {
                get => minutes;
                set => minutes = (value >= 0 && value <= 59) ? value : throw new ArgumentOutOfRangeException("Минуты", "Минуты должны быть между 0 и 59.");
            }

            public DialClockArray() : this(12, 0) { }

            public DialClockArray(int hours, int minutes)
            {
                Hours = hours;
                Minutes = minutes;
                objectCount++;
            }

            public static double CalculateAngle(int hours, int minutes)
            {
                double angle = Math.Abs(30 * hours - (11 / 2.0) * minutes);
                return Math.Round(angle, 4);
            }

            public double CalculateAngle()
        {

            double hourAngle = (hours % 12) * 30 + (minutes * 0.5);
            double minuteAngle = minutes * 6;

            double angle = Math.Abs(hourAngle - minuteAngle);

            if (angle > 180)
            {
                angle = 360 - angle;
            }

            return Math.Round(angle, 4);
        }


        public static int GetObjectCount() => objectCount;

            public string GetInfo()
            {
                return $"Время: {hours}:{minutes:D2}\nУгол между часовой и минутной стрелками: {CalculateAngle()} градусов";
            }

            public static DialClockArray operator ++(DialClockArray dc)
            {
                dc.minutes++;
                if (dc.minutes >= 60)
                {
                    dc.minutes = 0;
                    dc.hours = (dc.hours + 1) % 24;
                }
                return dc;
            }

            public static DialClockArray operator --(DialClockArray dc)
            {
                dc.minutes--;
                if (dc.minutes < 0)
                {
                    dc.minutes = 59;
                    dc.hours = (dc.hours - 1 + 24) % 24;
                }
                return dc;
            }

        public static explicit operator bool(DialClockArray dc)
        {
            double angle = dc.CalculateAngle();
            angle = Math.Round(angle, 2);
            double remainder = angle % 2.5;

            Console.WriteLine($"Angle: {angle}, Remainder: {remainder}");

            return Math.Abs(remainder) < 0.01 || Math.Abs(remainder - 2.5) < 0.01;
        }

        public static explicit operator int(DialClockArray dc)
            {
                return dc.hours * 60 + dc.minutes;
            }

            public static DialClockArray operator +(DialClockArray dc, int minutes)
            {
                dc.minutes += minutes;
                if (dc.minutes >= 60)
                {
                    dc.hours += dc.minutes / 60;
                    dc.minutes %= 60;
                }
                return dc;
            }

            public static DialClockArray operator -(DialClockArray dc, int minutes)
            {
                dc.minutes -= minutes;
                if (dc.minutes < 0)
                {
                    dc.hours -= (Math.Abs(dc.minutes) / 60) + 1;
                    dc.minutes = 60 - Math.Abs(dc.minutes) % 60;
                }
                return dc;
            }

            public void DisplayInfo()
            {
                Console.WriteLine($"Время: {hours}:{minutes:D2}");
                Console.WriteLine($"Угол между часовой и минутной стрелками: {CalculateAngle()} градусов");
            }

            public void Show()
            {
                DisplayInfo();
            }

        private double[] angles;

        public DialClockArray(double[] angles)
        {
            this.angles = angles;
        }

        public bool HasLargeAngles()
        {
            foreach (var angle in angles)
            {
                if (angle <= 360)
                    return false;
            }
            return true;
        }

        public bool HasNonMultiplesOf2_5()
        {
            foreach (var angle in angles)
            {
                if (angle % 2.5 == 0)
                    return false;
            }
            return true;
        }
    }

        public class DialClock
        {


        private DialClockArray[] arr;

            public DialClock()
            {
                arr = new DialClockArray[0];
            }

        private static int randomCount = 0;

            public DialClock(int size)
            {
                arr = new DialClockArray[size];
                Random rand = new Random();
                for (int i = 0; i < size; i++)
                {
                    arr[i] = new DialClockArray(rand.Next(0, 23), rand.Next(0, 59));
                    randomCount++;
                }
            }

        public static int GetRandomCount() => randomCount;

        public DialClock(DialClock other)
            {
                arr = new DialClockArray[other.arr.Length];
                for (int i = 0; i < other.arr.Length; i++)
                {
                    arr[i] = new DialClockArray(other.arr[i].Hours, other.arr[i].Minutes);
                }
            }

            public int Length => arr.Length;

            public void DisplayElements()
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.WriteLine($"Часы {i + 1}: {arr[i].Hours}:{arr[i].Minutes:D2}, Угол: {arr[i].CalculateAngle()} градусов");
                }
            }

            public DialClockArray this[int index]
            {
                get
                {
                    if (index < 0 || index >= arr.Length)
                        throw new Exception("Индекс вне предела");
                    return arr[index];
                }
                set
                {
                    if (index < 0 || index >= arr.Length)
                        throw new Exception("Индекс вне предела");
                    arr[index] = value;
                }
            }

            public DialClockArray FindMaxAngle()
            {
                if (arr.Length == 0)
                    return null;

                DialClockArray maxClock = arr[0];
                foreach (var clock in arr)
                {
                    if (clock.CalculateAngle() > maxClock.CalculateAngle())
                        maxClock = clock;
                }
                return maxClock;
            }
        }
}
