using Grpc.Core;
using GrpcService;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Polly;
using RedisLibrary;
using StackExchange.Redis;

namespace GrpcService.Services
{
    public class RedisService 
    {
        private readonly ILogger<RedisService> _logger;
        private IQueue _queue = null;
        public RedisService(ILogger<RedisService> logger)
        {
            _logger = logger;

            IAddress address = new Address("127.0.0.1", "6379");
            RedisLibrary.IConfiguration configuration = new Configuration(address, "redis-test");
            IConnectionFactory connectionFactory = new ConnectionFactory(configuration);

            _queue = new Queue(connectionFactory);
        }

        public async Task<bool> EnqueueAsync(string userInfo)
        {
            try
            {
                _queue.Enqueue(userInfo);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            await Task.CompletedTask;
            return true;
        }

        public async Task<bool> DequeueAsync(string userInfo)
        {
            try
            {
                _queue.Remove(userInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            await Task.CompletedTask;
            return true;
        }

    }
}
