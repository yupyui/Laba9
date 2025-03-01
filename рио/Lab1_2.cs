using Lab3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_2
{
    public class DialClock
    {
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

        public DialClock() : this(12, 0) { }

        public DialClock(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
            objectCount++;
        }

        public static double CalculateAngle(int hours, int minutes)
        {
            double hourAngle = 30 * (hours % 12) + 0.5 * minutes; 
                                                                
            double minuteAngle = 6 * minutes;

            double angle = Math.Abs(hourAngle - minuteAngle);

            if (angle > 180)
                angle = 360 - angle;

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

        public static DialClock operator ++(DialClock dc)
        {
            dc.minutes++;
            if (dc.minutes >= 60)
            {
                dc.minutes = 0;
                dc.hours = (dc.hours + 1) % 24;
            }
            return dc;
        }

        public static DialClock operator --(DialClock dc)
        {
            dc.minutes--;
            if (dc.minutes < 0)
            {
                dc.minutes = 59;
                dc.hours = (dc.hours - 1 + 24) % 24;
            }
            return dc;
        }

        public static explicit operator bool(DialClock dc)
        {
            return (dc.CalculateAngle() % 2.5 == 0);
        }

        public static explicit operator int(DialClock dc)
        {
            return dc.hours * 60 + dc.minutes;
        }

        public static DialClock operator +(DialClock dc, int minutes)
        {
            dc.minutes += minutes;
            if (dc.minutes >= 60)
            {
                dc.hours += dc.minutes / 60;
                dc.minutes %= 60;
            }
            return dc;
        }

        public static DialClock operator -(DialClock dc, int minutes)
        {
            dc.minutes -= minutes;
            if (dc.minutes < 0)
            {
                dc.hours -= (Math.Abs(dc.minutes) / 60) + 1;
                dc.minutes = 60 - Math.Abs(dc.minutes) % 60;
            }
            return dc;
        }

        public void Show()
        {
            Console.WriteLine($"Текущее время: {Hours}:{Minutes:D2}, Угол: {CalculateAngle()} градусов");
        }
    }
}
