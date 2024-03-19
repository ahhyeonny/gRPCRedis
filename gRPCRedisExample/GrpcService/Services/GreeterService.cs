using Grpc.Core;
using GrpcService;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Polly;
using RedisLibrary;
using StackExchange.Redis;

namespace GrpcService.Services
{
    public class GreeterService 
    {
        private readonly ILogger<GreeterService> _logger;
        private IQueue _queue = null;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;

            IAddress address = new Address("127.0.0.1", "6379");
            RedisLibrary.IConfiguration configuration = new Configuration(address, "redis-test");
            IConnectionFactory connectionFactory = new ConnectionFactory(configuration);

            _queue = new Queue(connectionFactory);
        }

        public async Task<string> EnqueueAsync(string userInfo)
        {
            _queue.Enqueue(userInfo);
            await Task.CompletedTask;
            return "Sucess";
        }


    }
}
