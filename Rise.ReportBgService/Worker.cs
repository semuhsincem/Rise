using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.ReportBgService
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
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };

                    using (IConnection connection = factory.CreateConnection())
                    using (IModel channel = connection.CreateModel())
                    {
                        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                        channel.BasicConsume("MckReport", false, consumer);
                        consumer.Received += (sender, e) =>
                        {
                            //Thread.Sleep(int.Parse(args[0]));
                            var data = e.Body.ToArray();
                            Console.WriteLine(Encoding.UTF8.GetString(data) + " alýndý");
                            //Create Excel And Send Email
                            channel.BasicAck(e.DeliveryTag, false);
                        };
                        Console.Read();
                    }
                    #region Delay The Process
                    await Task.Delay(1000, stoppingToken);
                    #endregion
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
