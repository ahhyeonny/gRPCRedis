using GrpcService.Services;
using MediatR;
using LanguageExt;
using GrpcService.Request;
using Infrastructure.Grpc;
using GrpcService.Command;

namespace GrpcService.Handler
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, Option<string>>
    {
        private readonly RedisService _redisService;

        public AddUserHandler(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Option<string>> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {
            var entity = JoinUserInfo(command.request);
            return await _redisService.EnqueueAsync(entity) == true ? Option<string>.Some("Success") : Option<string>.None;
        }
    }
}
