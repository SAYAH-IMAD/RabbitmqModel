using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace worker
{
    public class FirstWorker
    {
        public FirstWorker()
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
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model,message)=>
                    {
                        var receivedMessage = Encoding.UTF8.GetString(message.Body);
                        Thread.Sleep(1000);
                        System.Console.WriteLine($"First Worker {receivedMessage}");
                    };

                    channel.BasicConsume(
                        queue:"task_worker",
                        autoAck:true,
                        consumer:consumer
                    );
                }
            }
        }
    }
}