using Azure.Identity;
using Azure.Messaging.ServiceBus;
using SnakeNet_API.Services.Interfaces;

namespace SnakeNet_API.Services
{
    public class ServiceBusService : IMessageQueueService
	{
		private readonly ServiceBusSender _serviceBusSender;
		private readonly ILogger _logger;
		private readonly ServiceBusClientOptions clientOptions = new ServiceBusClientOptions
		{
			TransportType = ServiceBusTransportType.AmqpWebSockets
		};

		public ServiceBusService(ILogger<ServiceBusService> logger)
		{
			var _serviceBusClient = new ServiceBusClient(
				Environment.GetEnvironmentVariable("APPSETTING_ServiceBusNamespace"),
				new DefaultAzureCredential(),
				clientOptions);
			_serviceBusSender = _serviceBusClient.CreateSender(Environment.GetEnvironmentVariable("APPSETTING_ServiceBusQueue"));
			_logger = logger;
		}

		public async Task SendMessageAsync(string message, string title)
		{
			var serviceBusMessage = new ServiceBusMessage(message)
			{
				Subject = title,
			};

			await _serviceBusSender.SendMessageAsync(serviceBusMessage);
		}
	}
}
