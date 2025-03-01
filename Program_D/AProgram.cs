using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab3;
using Lab1_2;

namespace Program_D
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Создание объектов
            var clockArray = new DialClockArray(12, 30);
            var clock = new Lab3.DialClock(3);

            // Ручной ввод для clockArray
            Console.WriteLine("Введите время для clockArray:");
            clockArray.SetTimeManually();

            // Вывод информации
            Console.WriteLine("Демонстрация работы DialClockArray:");
            clockArray.Show();

            Console.WriteLine("\nДемонстрация работы DialClock:");
            clock.DisplayElements();

            Console.WriteLine($"\nКоличество рандомных чисел: {Lab3.DialClock.GetRandomCount()}");
        }
    }
}
