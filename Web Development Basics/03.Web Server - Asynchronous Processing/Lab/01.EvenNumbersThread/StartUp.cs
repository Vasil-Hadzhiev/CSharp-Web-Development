namespace _01.EvenNumbersThread
{
    using System;
    using System.Linq;
    using System.Threading;

    public class StartUp
    {
        public static void Main()
        {
            var tokens = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToArray();

            var start = tokens[0];
            var end = tokens[1];

            var thread = new Thread(() => PrintEvenNumbers(start, end));
            thread.Start();
            thread.Join();
            Console.WriteLine("Thread finished work");
        }

        private static void PrintEvenNumbers(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}