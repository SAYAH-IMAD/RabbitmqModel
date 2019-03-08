using System;
using RabbitMQ.Client;

namespace worker
{
    class Program
    {
        static void Main(string[] args)
        {
            FirstWorker fw = new FirstWorker();
            SecondWorker sw = new SecondWorker();
            System.Console.WriteLine("Tak worker process");
            Console.ReadLine();
        }
    }
}
