using HiveMQtt.Client.Options;
using HiveMQtt.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using NABD.Models.Domain;
using NABD.Data;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace NABD.MQTT
{
    public class MQTTBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private HiveMQClient? _client;

        public MQTTBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var options = new HiveMQClientOptions()
            {
                Host = "fc8731aef5304f3aad37fa03be01e517.s1.eu.hivemq.cloud",
                Port = 8883,
                UserName = "finalProject",
                Password = "Fb123456",
                UseTLS = true
            };

            _client = new HiveMQClient(options);

            _client.OnMessageReceived += async (sender, args) =>
            {
                string? topic = args.PublishMessage.Topic;
                string message = args.PublishMessage.PayloadAsString;
                DateTime timestamp = DateTime.UtcNow;

                // Set Default Values
                int SpO2 = 98;
                int HR = 70;
                float Temp = 37.0f;
                int ToolId = 1; // 🔹 Assign a ToolId (Modify based on your logic)

                // Parse and store data in the database
                await SaveToDatabase(topic, message, timestamp, SpO2, HR, Temp, ToolId);
            };

            var connectResult = await _client.ConnectAsync().ConfigureAwait(false);

            if (connectResult.ReasonCode == HiveMQtt.MQTT5.ReasonCodes.ConnAckReasonCode.Success)
            {
                await _client.SubscribeAsync("sensor/temperature").ConfigureAwait(false);
                await _client.SubscribeAsync("sensor/heart_rate").ConfigureAwait(false);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }

            await _client.DisconnectAsync().ConfigureAwait(false);
        }

        private async Task SaveToDatabase(string topic, string message, DateTime timestamp, int SpO2, int HR, float Temp, int ToolId)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // 🔹 Count total readings
            int totalReadings = await db.MQTTMessages.CountAsync();

            // 🔹 If more than 360, delete the oldest ones
            if (totalReadings >= 360)
            {
                var oldestReadings = db.MQTTMessages.OrderBy(m => m.VitalDataTimestamp).Take(totalReadings - 360);
                db.MQTTMessages.RemoveRange(oldestReadings);
                await db.SaveChangesAsync();
            }

            var mqttData = new MQTTMessage
            {
                Topic = topic,
                Message = message,
                VitalDataTimestamp = timestamp,
                OxygenSaturation = SpO2,
                HeartRate = HR,
                BodyTemperature = Temp,
                ToolId = ToolId // 🔹 Store the associated Tool
            };

            if (topic == "sensor/heart_rate")
            {
                var match = Regex.Match(message, @"Heart rate: (\d+\.?\d*) bpm, SpO2: (\d+\.?\d*)%");
                if (match.Success)
                {
                    mqttData.HeartRate = (int)float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                    mqttData.OxygenSaturation = (int)float.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);

                }
            }
            else if (topic == "sensor/temperature")
            {
                var match = Regex.Match(message, @"Ambient: (\d+\.?\d*) °C, Object: (\d+\.?\d*) °C");
                if (match.Success)
                {
                    mqttData.BodyTemperature = float.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
                }
            }

            db.MQTTMessages.Add(mqttData);
            await db.SaveChangesAsync();
        }
    }
}
