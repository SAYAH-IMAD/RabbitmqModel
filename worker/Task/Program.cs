using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace task
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new string[] 
            {
                "123",
                "123",
                "456",
                "456",
                "789"
            };

            ConnectionFactory connectionFactory = new ConnectionFactory();

            connectionFactory.UserName="guest";
            connectionFactory.Password="guest";
            connectionFactory.HostName ="localhost";
            connectionFactory.VirtualHost="/";   

            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue:"task_worker",
                                        durable:true,
                                        exclusive:false,
                                        autoDelete:false,
                                        arguments:null);

                    var message = GetMessage(args);
                    var body = Encoding.UTF8.GetBytes(message);

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    for (int i = 0; i < 100; i++)
                    {
                        channel.BasicPublish(exchange: "",
                                                  routingKey: "task_worker",
                                                  mandatory: false,
                                                  basicProperties: properties,
                                                  body: body); 
                    }



                }
            }          
            Console.WriteLine(args);
            Console.ReadLine();
        }

         private static string GetMessage(string[] args)
        {
        return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}
