using Grpc.Core;
using GrpcService;

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
            return Task.FromResult(new HelloReply { Message = "Sucess Save Redis"});
        }
    }
}
