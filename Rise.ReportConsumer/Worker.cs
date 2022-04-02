using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.ReportConsumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                ConnectionFactory factory = new ConnectionFactory() { HostName="localhost" };
               
                using (IConnection connection = factory.CreateConnection())
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare("MckReport", durable: true, false, false, null);
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume("MckReport", false, consumer);
                    consumer.Received += (sender, e) =>
                    {
                        //Thread.Sleep(int.Parse(args[0]));
                        var data = e.Body.ToArray();
                        Console.WriteLine(Encoding.UTF8.GetString(data) + " alýndý");
                        channel.BasicAck(e.DeliveryTag, false);
                    };
                    Console.Read();
                }
                #region Delay The Process
                await Task.Delay(1000, stoppingToken);
                #endregion
            }
        }

    }
}
