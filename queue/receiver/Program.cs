using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            
            connectionFactory.UserName="guest";
            connectionFactory.Password="guest";
            connectionFactory.HostName ="localhost";
            connectionFactory.VirtualHost="/";   

            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    int i = 0;
                    channel.QueueDeclare(queue: "learnrabbit",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);

                        Console.WriteLine($" message {i} Received {message}");
                        i++;
                    };
                    channel.BasicConsume(queue: "learnrabbit",
                                        autoAck: false,
                                        consumer: consumer);
                                        
                    Console.WriteLine("project receiver");

                    Console.ReadKey();

                }
                
            }
        }
    }
}
