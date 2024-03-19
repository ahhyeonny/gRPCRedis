using Grpc.Core;
using GrpcService;
using RedisLibrary;

namespace GrpcService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> AddUser(UserInfo request, ServerCallContext context)
        {
            IAddress address = new Address("0.0.0.0", "6379");
            RedisLibrary.IConfiguration configuration = new Configuration(address, "redis-test");
            IConnectionFactory connectionFactory = new ConnectionFactory(configuration);

            var _queue = new Queue(connectionFactory);


            return Task.FromResult(new HelloReply { Message = "Sucess Save Redis"});
        }
    }
}
