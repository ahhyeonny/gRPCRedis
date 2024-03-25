using GrpcService.Services;
using MediatR;
using LanguageExt;
using GrpcService.Request;
using Infrastructure.Grpc;
using GrpcService.Command;

namespace GrpcService.Handler
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Option<string>>
    {
        private readonly RedisService _redisService;

        public DeleteUserHandler(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Option<string>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var entity = JoinUserInfo(command.request);
            return await _redisService.DequeueAsync(entity) == true ? Option<string>.Some("Success") : Option<string>.None;
        }

        private string JoinUserInfo(UserInfo userInfo) 
        {
            return $"{userInfo.Id},{userInfo.Name},{userInfo.Email},{userInfo.Password}";
        }
    }
}
