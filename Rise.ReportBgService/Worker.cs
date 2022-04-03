using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestSharp;
using Rise.Helper;
using Rise.ViewModels;
using Rise.ViewModels.ServiceResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
                ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
                IConnection connection = factory.CreateConnection();
                IModel channel = connection.CreateModel();
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume("MckReport", false, consumer);
                consumer.Received += (sender, e) =>
                {
                   
                    var rawData = e.Body.ToArray();
                    var data = Encoding.UTF8.GetString(rawData);

                    var res = JsonConvert.DeserializeObject<ReportViewModel>(data);

                    //Create Excel
                    var filePath = CreateExcelDocument.CreateEx(res.Location);
                    var client = new RestClient("https://localhost:44302/api");
                    var request = new RestRequest($"/Report/ChangeReportStatus/{res.Id}");

                    var response = client.GetAsync(request).Result;
                    var changeStatusData = new ServiceResult<bool>();
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        changeStatusData = JsonConvert.DeserializeObject<ServiceResult<bool>>(response.Content);
                    }
                    //SendEmail
                    SenderService.SendMail(filePath, res.EmailAddress);
                    channel.BasicAck(e.DeliveryTag, false);
                };
                #region Delay The Process
                await Task.Delay(10000, stoppingToken);
                #endregion
            }
        }
    }
}
