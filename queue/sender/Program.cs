using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace sender
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

                       channel.QueueDeclare(queue: "learnrabbit",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    string message = "Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    

                    for (int i = 0; i < 1000000; i++)
                    {
                        channel.BasicPublish(exchange: "",
                                        routingKey: "learnrabbit",
                                        basicProperties: null,
                                        body: body);
                                        
                        Console.WriteLine($" message {i} Sent {message}");
                    }
                  
                }
            }
          
              
            Console.WriteLine("project sender");
        }
    }
}
