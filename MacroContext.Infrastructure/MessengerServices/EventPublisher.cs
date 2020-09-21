using MacroContext.Contract.Events;
using MacroContext.Infrastructure.CrossCuttingConcerns;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroContext.Infrastructure.MessengerServices
{
    public class EventPublisher: lEventPublisher
    {
        public string RoutingKey { get; set; }
        public void Publish(TransactionResult eventResult)
        {
            ExternalPublish(eventResult);
        }

        private void ExternalPublish(TransactionResult eventResult)
        {

            var connectionFactory = Bootstrapper.Container.GetInstance<ConnectionFactory>();
            using (var connection = connectionFactory.CreateConnection("Pub_MacroContext"))
            {
                using (var _channel = connection.CreateModel())
                {
                    string _exchangeName = AppSettings.RABBITMQ_EXCHANGE_NAME;

                    _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);
                    var jsonSerializerSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
                    var jsonified = JsonConvert.SerializeObject(eventResult, jsonSerializerSettings);
                    byte[] eventBuffer = Encoding.UTF8.GetBytes(jsonified);

                    _channel.BasicPublish(exchange: _exchangeName,
                                         routingKey: RoutingKey,//e.GetType().ToString(),
                                         basicProperties: null,
                                         body: eventBuffer);
                }

            }

        }
    }
}
