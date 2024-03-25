using GrpcService.Services;
using MediatR;
using LanguageExt;
using GrpcService.Request;
using Infrastructure.Grpc;
using GrpcService.Command;

namespace GrpcService.Handler
{
    public class DequeueUserHandler : IRequestHandler<DequeueUserCommand, string>
    {
        private readonly RedisService _redisService;

        public DequeueUserHandler(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<string> Handle(DequeueUserCommand command, CancellationToken cancellationToken)
        {
            return await _redisService.DequeueAsync();
        }
    }
}
