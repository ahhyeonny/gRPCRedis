using Grpc.Core;
using GrpcService;
using GrpcService.model;
using Microsoft.AspNetCore.Components.Forms;
using Polly;
using RedisLibrary;
using StackExchange.Redis;

namespace GrpcService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        public List<QueueItem> QueueItems = new List<QueueItem>();
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

        public override Task<HelloReply> AddUser(UserInfo request, ServerCallContext context)
        {
            //queueø° request ¿˙¿Â
            var userInfo = JoinUserInfo(request);
            if(_queue == null)
                return Task.FromResult(new HelloReply { Message = "Fail" });

            _queue.Enqueue(userInfo);
            return Task.FromResult(new HelloReply { Message = "Sucess"});
        }

        public static string JoinUserInfo(UserInfo userInfo)
        {
            return $"ID : {userInfo.Id}, Name : {userInfo.Name}, Email : {userInfo.Email}, Password : {userInfo.Password}";
        }
    }
}
